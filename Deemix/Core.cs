using Deemix.Exceptions;
using E.Deezer;

namespace Deemix;

public class DeemixClient
{
    public static async Task<DeemixClient> Create(string arl)
    {
        DeemixClient client = new(arl);

        await client._session.Login(arl, CancellationToken.None);

        if (!client._session.IsAuthenticated)
            throw new InvalidARLException("The given ARL is invalid, downloading is not possible.");

        return client;
    }

    private DeemixClient(string arl)
    {
        _arl = arl;
        _session = new(null);
    }

    private string _arl;
    private DeezerSession _session;

    public void Download(string url, string downloadPath)
    {

    }
}