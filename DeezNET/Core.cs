using DeezNET.Data;
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
    }

    private Bitrate _bitrate;
    private HttpClient _client;
    private GWApi _api;

    public async Task DownloadToFile(string url, string downloadPath)
    {
        TrackPage page = await _api.GetTrackPage(1903638027);

        return;
        FileStream stream = new(@"C:\Users\trevo\Desktop\encrypted.flac", FileMode.Open);
        FileStream stream2 = new(@"C:\Users\trevo\Desktop\out.flac", FileMode.Create);

        string blowfishKey = Decryption.GenerateBlowfishKey("1903638027"); // track id, public info

        // TODO: Crypted
        bool isCrypted = true;

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
}