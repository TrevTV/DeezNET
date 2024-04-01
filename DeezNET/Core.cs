using DeezNET.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Authentication;
using System.Text;

namespace DeezNET;

public class DeezerClient
{
    public static async Task<DeezerClient> Create(string arl, Bitrate bitrate = Bitrate.MP3_320)
    {
        DeezerClient client = new(arl, bitrate);
        await client._api.SetToken();
        return client;
    }

    private DeezerClient(string arl, Bitrate bitrate)
    {
        _bitrate = bitrate;
        _client = new HttpClient();
        _api = new(_client, arl);
        _arl = arl;
    }

    private Bitrate _bitrate;
    private HttpClient _client;
    private GWApi _api;
    private string _arl;

    public async Task DownloadToFile(string url, string downloadPath)
    {
        TrackPage page = await _api.GetTrackPage(401934352);
        TrackUrls urls = await GetTrackUrl(page.Data.TrackToken);

        Uri encryptedUri = urls.Data.First().Media.First().Sources.First().Url;

        HttpRequestMessage message = new(HttpMethod.Get, encryptedUri);
        HttpResponseMessage response = await _client.SendAsync(message);
        Stream stream = await response.Content.ReadAsStreamAsync();

        FileStream stream2 = new(@"C:\Users\trevo\Desktop\out.flac", FileMode.Create);

        string blowfishKey = Decryption.GenerateBlowfishKey("401934352"); // track id, public info

        bool isCrypted = encryptedUri.ToString().Contains("/mobile/") || encryptedUri.ToString().Contains("/media/");

        bool isStart = true;

        int bytesRead;
        byte[] buffer = new byte[2048 * 3];
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            if (isCrypted && bytesRead >= 2048)
            {
                buffer = Decryption.DecryptChunk(blowfishKey, buffer.AsSpan(0, 2048)).ToArray()
                        .Concat(buffer.AsSpan(2048).ToArray()).ToArray();
            }

            if (isStart && buffer[0] == 0 && !Encoding.UTF8.GetString(buffer, 4, 4).Equals("ftyp"))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] != 0)
                    {
                        buffer = new ArraySegment<byte>(buffer, i, buffer.Length - i).ToArray();
                        break;
                    }
                }
            }

            isStart = false;

            stream2.Write(buffer, 0, buffer.Length);
        }
    }

    private async Task<TrackUrls> GetTrackUrl(string token)
    {
        GetUrlRequestBody reqData = new()
        {
            LicenseToken = _api.ActiveUserData["USER"]!["OPTIONS"]!["license_token"]!.ToString(),
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
                            FormatFormat = _bitrate.ToString()
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