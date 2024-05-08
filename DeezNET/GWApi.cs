using DeezNET.Data;
using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace DeezNET;

public class GWApi
{
    internal GWApi(HttpClient client, string arl)
    {
        _client = client;
        _arl = arl;
        _apiToken = "null"; // this doesn't necessarily need to be null; used for deezer.getUserData
    }

    public JToken? ActiveUserData { get => _activeUserData; }

    internal string _arl;
    internal string _apiToken;
    private HttpClient _client;
    private JToken? _activeUserData;

    internal async Task SetToken()
    {
        if (string.IsNullOrEmpty(_arl))
            return;

        JToken userData = await GetUserData();
        _activeUserData = userData;
        _apiToken = userData["checkForm"]!.ToString();
    }

    public async Task<JToken> GetUserData() => (await Call("deezer.getUserData", needsArl: true));

    public async Task<JToken> GetUserProfilePage(long userId, string tab, int limit = 10) => await Call("deezer.pageProfile", new()
    {
        ["USER_ID"] = userId,
        ["tab"] = tab,
        ["nb"] = limit
    });

    public async Task<JToken> GetUserFavoriteIds(int limit = 10000, int start = 0, string? checksum = null) => await Call("song.getFavoriteIds", new()
    {
        ["start"] = start,
        ["checksum"] = checksum,
        ["nb"] = limit
    });

    public async Task<JToken> GetChildAccounts() => await Call("deezer.getChildAccounts");

    public async Task<JToken> GetTrackPage(long songId) => (await Call("deezer.pageTrack", new() { ["SNG_ID"] = songId }));

    public async Task<JToken> GetTrack(long songId) => await Call("song.getData", new()
    {
        ["SNG_ID"] = songId
    });

    public async Task<JToken> GetTracks(long[] songIds) => await Call("song.getListData", new()
    {
        ["SNG_IDS"] = new JArray(songIds)
    });

    public async Task<JToken> GetTrackLyrics(long songId) => await Call("song.getLyrics", new()
    {
        ["SNG_ID"] = songId
    });

    // 'us' is not a language, i know, but it is what deezer sends to the endpoint apparently
    // it doesn't seem to change much anyway, the Accept-Language header seems to be used instead
    public async Task<JToken> GetAlbumPage(long albumId) => (await Call("deezer.pageAlbum", new() { ["ALB_ID"] = albumId, ["LANG"] = "us" }));

    public async Task<JToken> GetAlbum(long albId) => await Call("album.getData", new()
    {
        ["ALB_ID"] = albId
    });

    public async Task<JToken> GetAlbumTracks(long albId) => await Call("song.getListByAlbum", new()
    {
        ["ALB_ID"] = albId,
        ["nb"] = -1,
    });

    public async Task<JToken> GetArtistPage(long artistId) => (await Call("deezer.pageArtist", new() { ["ART_ID"] = artistId, ["LANG"] = "us" }));

    public async Task<JToken> GetArtist(long artId) => await Call("artist.getData", new()
    {
        ["ART_ID"] = artId
    });

    public async Task<JToken> GetArtistTopTracks(long artId, int limit = 100) => await Call("artist.getTopTrack", new()
    {
        ["ART_ID"] = artId,
        ["nb"] = limit
    });

    public async Task<JToken> GetArtistDiscography(long artId, int index = 0, int limit = 25) => await Call("artist.getDiscography", new()
    {
        ["ART_ID"] = artId,
        ["discography_mode"] = "all",
        ["nb"] = limit,
        ["nb_songs"] = 0,
        ["start"] = index,
    });

    public async Task<JToken> GetPlaylistPage(long playlistId) => await Call("deezer.pagePlaylist", new()
    {
        ["PLAYLIST_ID"] = playlistId,
        ["lang"] = "en"
    });

    public async Task<JToken> GetPlaylistTracks(long playlistId) => await Call("playlist.getSongs", new()
    {
        ["PLAYLIST_ID"] = playlistId,
        ["nb"] = -1
    });

    public async Task<JToken> CreatePlaylist(string title, string description, long[] trackIds, PlaylistStatus status = PlaylistStatus.PUBLIC) => await Call("playlist.create", new()
    {
        ["title"] = title,
        ["description"] = description,
        ["status"] = (int)status,
        ["songs"] = new JArray(trackIds.Select(t => new JArray(t.ToString())))
    });

    public async Task<JToken> EditPlaylist(long playlistId, string title, PlaylistStatus status, string description, long[] trackIds) => await Call("playlist.update", new()
    {
        ["PLAYLIST_ID"] = playlistId,
        ["title"] = title,
        ["description"] = description,
        ["status"] = (int)status,
        ["songs"] = new JArray(trackIds.Select(t => new JArray(t.ToString())))
    });

    public async Task<JToken> AddSongsToPlaylist(long playlistId, long[] trackIds, int offset = -1) => await Call("playlist.addSongs", new()
    {
        ["PLAYLIST_ID"] = playlistId,
        ["songs"] = new JArray(trackIds.Select(t => new JArray(t.ToString()))),
        ["offset"] = offset
    });

    public async Task<JToken> AddSongToPlaylist(long playlistId, long trackId, int offset = -1) => await AddSongsToPlaylist(playlistId, [trackId], offset);

    public async Task<JToken> RemoveSongsFromPlaylist(long playlistId, long[] trackIds) => await Call("playlist.deleteSongs", new()
    {
        ["PLAYLIST_ID"] = playlistId,
        ["songs"] = new JArray(trackIds.Select(t => new JArray(t.ToString()))),
    });

    public async Task<JToken> RemoveSongFromPlaylist(long playlistId, long trackId) => await RemoveSongsFromPlaylist(playlistId, [trackId]);

    public async Task<JToken> DeletePlaylist(long playlistId) => await Call("playlist.delete", new()
    {
        ["PLAYLIST_ID"] = playlistId
    });

    public async Task<JToken> AddSongToFavorites(long songId) => await Call("favorite_song.add", new()
    {
        ["SNG_ID"] = songId
    });

    public async Task<JToken> RemoveSongToFavorites(long songId) => await Call("favorite_song.remove", new()
    {
        ["SNG_ID"] = songId
    });

    public async Task<JToken> AddAlbumToFavorites(long albumId) => await Call("album.addFavorite", new()
    {
        ["ALB_ID"] = albumId
    });

    public async Task<JToken> RemoveAlbumToFavorites(long albumId) => await Call("album.deleteFavorite", new()
    {
        ["ALB_ID"] = albumId
    });

    public async Task<JToken> AddArtistToFavorites(long artistId) => await Call("artist.addFavorite", new()
    {
        ["ART_ID"] = artistId
    });

    public async Task<JToken> RemoveArtistToFavorites(long artistId) => await Call("artist.deleteFavorite", new()
    {
        ["ART_ID"] = artistId
    });

    public async Task<JToken> AddPlaylistToFavorites(long playlistId) => await Call("playlist.addFavorite", new()
    {
        ["PARENT_PLAYLIST_ID"] = playlistId
    });

    public async Task<JToken> RemovePlaylistToFavorites(long playlistId) => await Call("playlist.deleteFavorite", new()
    {
        ["PLAYLIST_ID"] = playlistId
    });

    public async Task<JToken> GetPage(string page) => await Call("page.get", parameters: new()
    {
        { "gateway_input", new JObject()
            {
                ["PAGE"] = page,
                ["VERSION"] = "2.3",
                ["SUPPORT"] = new JObject()
                {
                    ["grid"] = new JArray()
                    {
                        "channel",
                        "album"
                    },
                    ["horizontal-grid"] = new JArray()
                    {
                        "album"
                    }
                },
                ["LANG"] = "us"
            }.ToString()
        }
    });

    public async Task<JToken> Search(string query, int index = 0, int limit = 10, bool suggest = true, bool artistSuggest = true, bool topTracks = true) => await Call("deezer.pageSearch", new()
    {
        ["query"] = query,
        ["start"] = index,
        ["nb"] = limit,
        ["suggest"] = suggest,
        ["artist_suggest"] = artistSuggest,
        ["top_tracks"] = topTracks,
    });

    public async Task<JToken> SearchMusic(string query, string type, int index = 0, int limit = 10) => await Call("search.music", new()
    {
        ["query"] = query,
        ["start"] = index,
        ["nb"] = limit,
        ["output"] = type,
        ["filter"] = "ALL"
    });

    private async Task<JToken> Call(string method, JObject? args = null, Dictionary<string, string>? parameters = null, bool needsArl = false)
    {
        if (string.IsNullOrEmpty(_arl))
            throw new InvalidARLException("A GWApi method is attempting to be called without being provided an ARL.");

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
            string key = WebUtility.UrlEncode(parameters.ElementAt(i).Key);
            string value = WebUtility.UrlEncode(parameters.ElementAt(i).Value);
            stringBuilder.Append(start + key + "=" + value);
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

            throw new APIException(error.ToString());
        }

        return json["results"]!;
    }
}