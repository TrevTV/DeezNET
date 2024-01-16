using Deemix.Data;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto;

namespace Deemix;

public class DeemixClient
{
    public static async Task<DeemixClient> Create(string arl)
    {
        DeemixClient client = new(arl);
        return client;
    }

    private DeemixClient(string arl, Bitrate bitrate = Bitrate.MP3_320)
    {
        _arl = arl;
        _bitrate = bitrate;
    }

    private string _arl;
    private Bitrate _bitrate;

    public async Task Download(string url, string downloadPath)
    {
        FileStream stream = new(@"C:\Users\trevo\Desktop\encrypted.flac", FileMode.Open);
        FileStream stream2 = new(@"C:\Users\trevo\Desktop\out.flac", FileMode.Create);

        string blowfishKey = GenerateBlowfishKey("1903638027"); // track id, public info

        // TODO: Crypted
        bool isCrypted = true;

        bool isStart = true;

        int bytesRead;
        byte[] buffer = new byte[2048 * 3];
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            if (isCrypted && bytesRead >= 2048)
            {
                buffer = DecryptChunk(blowfishKey, buffer.AsSpan(0, 2048)).ToArray()
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

    private static string GenerateBlowfishKey(string trackId)
    {
        const string SECRET = "g4el58wc0zvf9na1";
        string md5Id = CalculateMD5(trackId);
        string bfKey = "";
        for (int i = 0; i < 16; i++)
            bfKey += (char)(md5Id[i] ^ md5Id[i + 16] ^ SECRET[i]);

        return bfKey;
    }

    public static string CalculateMD5(string data)
    {
        byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(data));
        StringBuilder sb = new();

        foreach (byte b in hashBytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }

    private static byte[] DecryptChunk(string key, ReadOnlySpan<byte> data)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        var engine = new BlowfishEngine();
        var cipher = new BufferedBlockCipher(new CbcBlockCipher(engine));
        var keyParam = ParameterUtilities.CreateKeyParameter("Blowfish", keyBytes);
        var parameters = new ParametersWithIV(keyParam, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 });
        cipher.Init(false, parameters);

        var decryptedBytes = new byte[cipher.GetOutputSize(data.Length)];
        var len = cipher.ProcessBytes(data.ToArray(), 0, data.Length, decryptedBytes, 0);
        len += cipher.DoFinal(decryptedBytes, len);

        Array.Resize(ref decryptedBytes, len);

        return decryptedBytes;
    }
}