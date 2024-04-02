using DeezNET.Data;
using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DeezNET
{
    public class GWApi
    {
        internal GWApi(HttpClient client, string arl)
        {
            _client = client;
            _arl = arl;
            _apiToken = "null"; // this doesn't necessarily need to be null; used for deezer.getUserData
        }

        public JToken ActiveUserData { get => _activeUserData; }

        private HttpClient _client;
        private string _arl;
        private string _apiToken;
        private JToken _activeUserData;

        internal async Task SetToken()
        {
            JToken userData = await GetUserData();
            _activeUserData = userData;
            _apiToken = userData["checkForm"]!.ToString();
        }

        // the UserData json response is cursed to the point where using a JToken directly is a better solution
        public async Task<JToken> GetUserData() => await Call("deezer.getUserData", needsArl: true);

        public async Task<JToken> GetUserProfilePage(int userId, int tab, int limit = 10) => await Call("deezer.getUserProfilePage", new()
        {
            ["USER_ID"] = userId,
            ["tab"] = tab,
            ["nb"] = limit
        });

        // TODO: a bunch of stuff i don't feel like implementing right now

        public async Task<TrackPage> GetTrackPage(int songId) => (await Call("deezer.pageTrack", new() { ["SNG_ID"] = songId })).ToObject<TrackPage>()!;

        private async Task<JToken> Call(string method, JObject? args = null, Dictionary<string, string>? parameters = null, bool needsArl = false)
        {
            parameters ??= [];
            parameters["api_version"] = "1.0";
            parameters["api_token"] = _apiToken;
            parameters["input"] = "3";
            parameters["method"] = method;

            string body = args == null ? "" : args.ToString();
            StringContent stringContent = new(body);

            StringBuilder stringBuilder = new("https://www.deezer.com/ajax/gw-light.php");
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

            if (needsArl)
                request.Headers.Add("Cookie", "arl=" + _arl);

            HttpResponseMessage response = await _client.SendAsync(request);

            string resp = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(resp);

            JToken? error = json["error"];
            if (error != null && error.Any())
            {
                if (error["VALID_TOKEN_REQUIRED"] != null || error["GATEWAY_ERROR"] != null)
                {
                    await SetToken();
                    return await Call(method, args, parameters);
                }

                if (error["DATA_ERROR"] != null)
                    throw new InvalidIDException("The given ID is not valid. It either does not exist or is not for the requested content type.");

                throw new Exception(error.ToString());
            }

            return json["results"]!;
        }
    }
}
