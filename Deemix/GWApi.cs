using Newtonsoft.Json.Linq;
using System.Text;

namespace Deemix
{
    public class GWApi
    {
        internal GWApi(HttpClient client)
        {
            _client = client;
            _apiToken = "null"; // this doesn't necessarily need to be null; used for deezer.getUserData
        }

        private HttpClient _client;
        private string _apiToken;

        internal async Task SetToken()
        {
            JToken userData = await GetUserData();
            _apiToken = userData["checkForm"]!.ToString();
        }

        public async Task<JToken> GetUserData() => await Call("deezer.getUserData");

        public async Task<JToken> GetUserProfilePage(int userId, int tab, int limit = 10) => await Call("deezer.getUserProfilePage", new()
        {
            ["USER_ID"] = userId,
            ["tab"] = tab,
            ["nb"] = limit
        });

        // TODO: a bunch of stuff i don't feel like implementing right now

        public async Task<JToken> GetTrackPage(int songId) => await Call("deezer.getTrackPage", new() { ["SNG_ID"] = songId });

        private async Task<JToken> Call(string method, JObject? args = null, Dictionary<string, string>? parameters = null)
        {
            parameters ??= [];
            parameters.Add("api_version", "1.0");
            parameters.Add("api_token", _apiToken);
            parameters.Add("input", "3");
            parameters.Add("method", method);

            string body = args == null ? "" : args.ToString();
            StringContent stringContent = new(body);

            StringBuilder stringBuilder = new("http://www.deezer.com/ajax/gw-light.php");
            for (int i = 0; i < parameters.Count; i++)
            {
                string start = i == 0 ? "?" : "&";
                stringBuilder.Append(start + parameters.ElementAt(i).Key + "=" + parameters.ElementAt(i).Value);
            }

            string url = stringBuilder.ToString();

            HttpRequestMessage request = new(HttpMethod.Post, url)
            {
                Content = stringContent
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            string resp = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(resp);

            if (json["error"]!.Any())
            {
                if (json["error"]!["VALID_TOKEN_REQUIED"] != null || json["error"]!["GATEWAY_ERROR"] != null)
                {
                    await SetToken();
                    return await Call(method, args, parameters);
                }
                else
                    // TODO: make this a custom exception
                    throw new Exception(json["error"]!.ToString());
            }

            return json["results"]!;
        }
    }
}
