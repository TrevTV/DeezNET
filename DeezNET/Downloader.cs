using DeezNET.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DeezNET.Metadata;
using DeezNET.Exceptions;

namespace DeezNET;

public class Downloader
{
    internal Downloader(HttpClient client, GWApi gw, PublicApi publicApi)
    {
        _client = client;
        _gw = gw;
        _publicApi = publicApi;
    }

    private HttpClient _client;
    private GWApi _gw;
    private PublicApi _publicApi;

    private const string CDN_TEMPLATE = "https://e-cdn-images.dzcdn.net/images/cover/{0}/{1}x{1}-000000-80-0-0.jpg";
    private readonly byte[] FLAC_MAGIC = "fLaC"u8.ToArray();

    /// <summary>
    /// Applies ID3 metadata to the given byte array.
    /// </summary>
    /// <param name="trackId">The track ID to base metadata on.</param>
    /// <param name="trackData">The track byte data to apply the metadata to.</param>
    /// <returns>The modified track data</returns>
    public async Task<byte[]> ApplyMetadataToTrackBytes(long trackId, byte[] trackData, int coverResolution = 512)
    {
        JToken page = await _gw.GetTrackPage(trackId);
        long albumId = long.Parse(page["DATA"]!["ALB_ID"]!.ToString());
        JToken albumPage = await _gw.GetAlbumPage(albumId);
        JToken publicAlbum = await _publicApi.GetAlbum(albumId);

        byte[]? albumArt = null;
        try { albumArt = await GetArtBytes(page["DATA"]!["ALB_PICTURE"]!.ToString(), coverResolution); } catch (UnavailableArtException) { }

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
        track.Tag.TrackCount = uint.Parse(albumPage["SONGS"]!["total"]!.ToString());
        // i have yet to find an album without a genre attached, but this may still break at some point
        track.Tag.Genres = publicAlbum["genres"]!["data"]!.Select(a => a["name"]!.ToString()).ToArray();

        if (albumArt != null)
            track.Tag.Pictures = [new TagLib.Picture(new TagLib.ByteVector(albumArt))];

        track.Save();

        byte[] attached = abstraction.MemoryStream.ToArray();

        abstraction.MemoryStream.Dispose();
        track.Dispose();
        return attached;
    }

    /// <summary>
    /// Downloads and decrypts track data for a given ID and bitrate.
    /// </summary>
    /// <param name="trackId">The track ID to download.</param>
    /// <param name="bitrate">The preferred bitrate to download.</param>
    /// <param name="fallback">The secondary bitrate choice if the preferred is unavailable.</param>
    /// <returns>The raw track data.</returns>
    /// <exception cref="NoSourcesAvailableException"></exception>
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

    /// <summary>
    /// Retrieves the art data for the given ID and resolution.
    /// </summary>
    /// <param name="id">The art ID.</param>
    /// <param name="resolution">The resolution for the image. Unavailable sizes will throw an UnavailableArtException.</param>
    /// <returns>The byte data of the art.</returns>
    /// <exception cref="UnavailableArtException">Occurs when the given art ID or resolution is unavailable.</exception>
    public async Task<byte[]> GetArtBytes(string id, int resolution)
    {
        HttpRequestMessage message = new(HttpMethod.Get, GetCDNUrl(id, resolution));
        HttpResponseMessage response = await _client.SendAsync(message);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnavailableArtException($"The art with {id} with resolution {resolution} is unavailable.");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Returns a different bitrate to act as a fallback.
    /// </summary>
    /// <param name="bitrate">The preferred bitrate.</param>
    /// <returns>The recommended fallback.</returns>
    public static Bitrate GetLowerFallbackBitrate(Bitrate bitrate)
    {
        return bitrate switch
        {
            Bitrate.FLAC => Bitrate.MP3_320,
            Bitrate.MP3_320 => Bitrate.MP3_128,
            Bitrate.MP3_128 => Bitrate.MP3_320,
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// Gets the CDN url for a given art ID.
    /// </summary>
    /// <param name="id">The art ID.</param>
    /// <param name="resolution">The resolution for the image. Unavailable sizes will return a 403 on request.</param>
    /// <returns>The CDN url.</returns>
    public static string GetCDNUrl(string id, int resolution)
    {
        return string.Format(CDN_TEMPLATE, id, resolution);
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

        request.Headers.Add("Cookie", "arl=" + _gw._arl);

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
