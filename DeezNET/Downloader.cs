using DeezNET.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DeezNET.Metadata;
using DeezNET.Exceptions;

namespace DeezNET;

public class Downloader
{
    internal Downloader(HttpClient client, string arl, GWApi gw, PublicApi publicApi)
    {
        _client = client;
        _arl = arl;
        _gw = gw;
        _publicApi = publicApi;
    }

    private HttpClient _client;
    private string _arl;
    private GWApi _gw;
    private PublicApi _publicApi;

    private const string CDN_TEMPLATE = "https://e-cdn-images.dzcdn.net/images/cover/{0}/{1}x{1}-000000-80-0-0.jpg";
    private readonly byte[] FLAC_MAGIC = "fLaC"u8.ToArray();

    public async Task<byte[]> ApplyMetadataToTrackBytes(long trackId, byte[] trackData)
    {
        JToken page = await _gw.GetTrackPage(trackId);
        long albumId = long.Parse(page["DATA"]!["ALB_ID"]!.ToString());
        JToken albumPage = await _gw.GetAlbumPage(albumId);
        JToken publicAlbum = await _publicApi.GetAlbum(albumId);

        byte[] albumArt = await _client.GetByteArrayAsync(string.Format(CDN_TEMPLATE, page["DATA"]!["ALB_PICTURE"]!.ToString(), 512));

        string ext = Enumerable.SequenceEqual(trackData[0..4], FLAC_MAGIC) ? ".flac" : ".mp3";

        FileBytesAbstraction abstraction = new("track" + ext, trackData);
        TagLib.File track = TagLib.File.Create(abstraction);
        track.Tag.Title = page["DATA"]!["SNG_TITLE"]!.ToString();
        track.Tag.Album = page["DATA"]!["ALB_TITLE"]!.ToString();
        track.Tag.Performers = page["DATA"]!["ARTISTS"]!.Select(a => a["ART_NAME"]!.ToString()).ToArray();
        track.Tag.AlbumArtists = albumPage["DATA"]!["ARTISTS"]!.Select(a => a["ART_NAME"]!.ToString()).ToArray();
        DateTime releaseDate = DateTime.Parse(page["DATA"]!["PHYSICAL_RELEASE_DATE"]!.ToString());
        track.Tag.Year = (uint)releaseDate.Year;
        track.Tag.Track = uint.Parse(page["DATA"]!["TRACK_NUMBER"]!.ToString());
        track.Tag.Pictures = [new TagLib.Picture(new TagLib.ByteVector(albumArt))];
        // i have yet to find an album without a genre attached, but this may still break at some point
        track.Tag.Genres = publicAlbum["genres"]!["data"]!.Select(a => a["name"]!.ToString()).ToArray();

        track.Save();

        byte[] attached = abstraction.MemoryStream.ToArray();

        abstraction.MemoryStream.Dispose();
        track.Dispose();
        return attached;
    }

    public async Task<byte[]> GetRawTrackBytes(long trackId, Bitrate bitrate, Bitrate? fallback = null)
    {
        JToken page = await _gw.GetTrackPage(trackId);
        TrackUrls urls = await GetTrackUrl(page["DATA"]!["TRACK_TOKEN"]!.ToString(), bitrate);

        Uri? encryptedUri = urls.Data.FirstOrDefault()?.Media.FirstOrDefault()?.Sources.FirstOrDefault()?.Url;
        if (encryptedUri == null)
        {
            if (fallback != null)
                return await GetRawTrackBytes(trackId, fallback.Value);
            throw new NoSourcesAvailableException($"Track ID {trackId} has no available media sources for bitrate {bitrate}.");
        }

        HttpRequestMessage message = new(HttpMethod.Get, encryptedUri);
        HttpResponseMessage response = await _client.SendAsync(message);
        Stream stream = await response.Content.ReadAsStreamAsync();

        MemoryStream outStream = new();

        string blowfishKey = Decryption.GenerateBlowfishKey(trackId.ToString());
        bool isCrypted = encryptedUri.ToString().Contains("/mobile/") || encryptedUri.ToString().Contains("/media/");

        Decryption.DecodeTrackStream(stream, outStream, isCrypted, blowfishKey);

        return outStream.ToArray();
    }

    private async Task<TrackUrls> GetTrackUrl(string token, Bitrate bitrate)
    {
        // with the order of this being called, this should never really be needed, but it ensures safety
        if (_gw.ActiveUserData == null)
            await _gw.SetToken();

        GetUrlRequestBody reqData = new()
        {
            LicenseToken = _gw.ActiveUserData!["USER"]!["OPTIONS"]!["license_token"]!.ToString(),
            TrackTokens = [token],
            Media =
            [
                new()
                {
                    Type = "FULL",
                    Formats = [
                        new()
                        {
                            Cipher = "BF_CBC_STRIPE",
                            FormatFormat = bitrate.ToString()
                        }
                    ]
                }
            ]
        };

        string reqDataSerialized = JsonConvert.SerializeObject(reqData);
        StringContent stringContent = new(reqDataSerialized);

        HttpRequestMessage request = new(HttpMethod.Post, "https://media.deezer.com/v1/get_url")
        {
            Content = stringContent
        };

        request.Headers.Add("Cookie", "arl=" + _arl);

        HttpResponseMessage response = await _client.SendAsync(request);

        string resp = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(resp);

        JToken? errors = json["errors"];
        if (errors != null && errors.Any())
        {
            throw new Exception(errors[0]!["message"]!.ToString());
        }

        return json.ToObject<TrackUrls>()!;
    }
}
