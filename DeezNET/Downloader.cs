using DeezNET.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DeezNET.Metadata;

namespace DeezNET;

public class Downloader
{
    internal Downloader(HttpClient client, string arl, GWApi gw)
    {
        _client = client;
        _arl = arl;
        _gw = gw;
    }

    private HttpClient _client;
    private string _arl;
    private GWApi _gw;

    private const string CDN_TEMPLATE = "https://e-cdn-images.dzcdn.net/images/cover/{0}/{1}x{1}-000000-80-0-0.jpg";
    private readonly byte[] FLAC_MAGIC = "fLaC"u8.ToArray();

    public async Task<byte[]> ApplyMetadataToTrackBytes(int trackId, byte[] trackData)
    {
        TrackPage page = await _gw.GetTrackPage(trackId);
        AlbumPage albumPage = await _gw.GetAlbumPage(int.Parse(page.Data.AlbId));
        byte[] albumArt = await _client.GetByteArrayAsync(string.Format(CDN_TEMPLATE, page.Data.AlbPicture, 512));

        string ext = Enumerable.SequenceEqual(trackData[0..4], FLAC_MAGIC) ? ".flac" : ".mp3";

        FileBytesAbstraction abstraction = new("track" + ext, trackData);
        TagLib.File track = TagLib.File.Create(abstraction);
        track.Tag.Title = page.Data.SngTitle;
        track.Tag.Album = page.Data.AlbTitle;
        track.Tag.Performers = page.Data.Artists.Select(a => a.ArtName).ToArray();
        track.Tag.AlbumArtists = albumPage.Data.Artists.Select(a => a.ArtName).ToArray();
        DateTime releaseDate = DateTime.Parse(page.Data.PhysicalReleaseDate);
        track.Tag.Year = (uint)releaseDate.Year;
        track.Tag.Track = uint.Parse(page.Data.TrackNumber);
        track.Tag.Pictures = [new TagLib.Picture(new TagLib.ByteVector(albumArt))];
        // TODO: genres
        // a simple get to https://api.deezer.com/album/{id} will give a `genres` list, if they have it defined

        track.Save();

        byte[] attached = abstraction.MemoryStream.ToArray();

        abstraction.MemoryStream.Dispose();
        track.Dispose();
        return attached;
    }

    public async Task<byte[]> GetRawTrackBytes(int trackId, Bitrate bitrate)
    {
        TrackPage page = await _gw.GetTrackPage(trackId);
        TrackUrls urls = await GetTrackUrl(page.Data.TrackToken, bitrate);

        // TODO: multiple sources, also possibly multiple tracks
        Uri encryptedUri = urls.Data.First().Media.First().Sources.First().Url;

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
        GetUrlRequestBody reqData = new()
        {
            LicenseToken = _gw.ActiveUserData.User.Options.LicenseToken,
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
