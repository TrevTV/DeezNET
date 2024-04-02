using DeezNET.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

    public async Task<byte[]> GetTrackBytes(int trackId, Bitrate bitrate)
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
            LicenseToken = _gw.ActiveUserData["USER"]!["OPTIONS"]!["license_token"]!.ToString(),
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
