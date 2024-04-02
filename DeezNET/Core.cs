using DeezNET.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeezNET;

public class DeezerClient
{
    public static async Task<DeezerClient> Create(string arl)
    {
        DeezerClient client = new(arl);
        await client._gw.SetToken();
        return client;
    }

    private DeezerClient(string arl)
    {
        _client = new HttpClient();
        _gw = new(_client, arl);
        _arl = arl;
        _downloader = new(_client, arl, _gw);
    }

    public Downloader Downloader { get => _downloader; }

    private Downloader _downloader;
    private HttpClient _client;
    private GWApi _gw;
    private string _arl;
}