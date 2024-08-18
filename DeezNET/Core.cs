[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DeezNET.Tests")]

namespace DeezNET;

public class DeezerClient
{
    /// <summary>
    /// Creates a new DeezerClient.
    /// </summary>
    public DeezerClient()
    {
        _arl = "";

        _clientHandler = new() { CookieContainer = new() };
        _client = new HttpClient(_clientHandler);
        _client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");

        _gwApi = new(_client, _arl);
        _publicApi = new(_client);
        _downloader = new(_client, _gwApi, _publicApi);
    }

    /// <summary>
    /// Sets the internal ARL and refreshes the API token.
    /// Passing a null, empty, or whitespace string will remove the ARL and API token from the GWApi.
    /// </summary>
    /// <param name="arl">A Deezer account access token.</param>
    public async Task SetARL(string arl)
    {
        if (string.IsNullOrWhiteSpace(arl))
        {
            _arl = "";
            _gwApi._arl = "";
            _gwApi._apiToken = "null";
        }

        _arl = arl;
        _gwApi._arl = arl;
        await _gwApi.SetToken();
    }

    public Downloader Downloader { get => _downloader; }
    public GWApi GWApi { get => _gwApi; }
    public PublicApi PublicApi { get => _publicApi; }
    public string ActiveARL { get => _arl; }
    public string SID { get => _clientHandler.CookieContainer.GetCookies(_deezerUri).FirstOrDefault(c => c.Name == "sid")?.Value ?? ""; }

    private Downloader _downloader;
    private HttpClient _client;
    private GWApi _gwApi;
    private PublicApi _publicApi;
    private string _arl;
    private HttpClientHandler _clientHandler;
    private readonly Uri _deezerUri = new("https://deezer.com");
}