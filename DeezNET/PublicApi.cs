using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DeezNET;

public class PublicApi
{
    internal PublicApi(HttpClient client)
    {
        _client = client;
    }

    private HttpClient _client;

    // TODO: theres an entire other api i gotta implement aswell :fire:
    // see deezer-py-main/deezer/api.py

    // TODO: stress testing functions, the models don't have many optionals and are based off a single json response so theres a high change newtonsoft will throw an error for something

    /*public async Task<JToken> GetUserProfilePage(int userId, string tab, int limit = 10) => await Call("deezer.pageProfile", new()
    {
        ["USER_ID"] = userId,
        ["tab"] = tab,
        ["nb"] = limit
    });*/

    private async Task<JToken> Call(string method, Dictionary<string, string>? parameters = null)
    {
        parameters ??= [];
        // TODO: parameters["access_token"] = _accessToken;

        StringBuilder stringBuilder = new("https://api.deezer.com/" + method);
        for (int i = 0; i < parameters.Count; i++)
        {
            string start = i == 0 ? "?" : "&";
            string key = WebUtility.UrlEncode(parameters.ElementAt(i).Key);
            string value = WebUtility.UrlEncode(parameters.ElementAt(i).Value);
            stringBuilder.Append(start + key + "=" + value);
        }

        string url = stringBuilder.ToString();

        HttpRequestMessage request = new(HttpMethod.Get, url);
        HttpResponseMessage response = await _client.SendAsync(request);

        string resp = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(resp);

        JToken? error = json["error"];
        if (error != null && error.Any())
        {
            JToken? code = error["code"];
            if (code != null)
            {
                // TODO: is this how you get the code?
                string? message = error["message"]?.ToString();
                int err = (int)code;
                switch (err)
                {
                    case 4: case 700:
                        {
                            await Task.Delay(5000);
                            return await Call(method, parameters);
                        }
                    case 100:
                        throw new ItemsLimitExceededException(message == null ? "" : message);
                    case 200:
                        throw new PermissionException(message == null ? "" : message);
                    case 300:
                        throw new InvalidTokenException(message == null ? "" : message);
                    case 500:
                        throw new WrongParameterException(message == null ? "" : message);
                    case 501:
                        throw new MissingParameterException(message == null ? "" : message);
                    case 600:
                        throw new InvalidQueryException(message == null ? "" : message);
                    case 800:
                        throw new Exceptions.DataException(message == null ? "" : message);
                    case 901:
                        throw new IndividualAccountChangedNotAllowedException(message == null ? "" : message);
                }
            }

            throw new APIException(error.ToString());
        }

        return json;
    }
}