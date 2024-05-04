namespace DeezNET;

public class DeezerClient
{
    public static async Task<DeezerClient> Create(string arl)
    {
        DeezerClient client = new(arl);
        await client._gwApi.SetToken();
        return client;
    }

    private DeezerClient(string arl)
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        _gwApi = new(_client, arl);
        _publicApi = new(_client);
        _arl = arl;
        _downloader = new(_client, arl, _gwApi, _publicApi);
    }

    public Downloader Downloader { get => _downloader; }
    public GWApi GWApi { get => _gwApi; }
    public PublicApi PublicApi { get => _publicApi; }

    private Downloader _downloader;
    private HttpClient _client;
    private GWApi _gwApi;
    private PublicApi _publicApi;
    private string _arl;
}