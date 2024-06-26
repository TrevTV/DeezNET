﻿using DeezNET.Data;
using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace DeezNET;

public class PublicApi
{
    internal PublicApi(HttpClient client)
    {
        _client = client;
    }

    private readonly HttpClient _client;

    public async Task<JToken> GetAlbum(long albumId, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"album/{albumId}", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetAlbumByUPC(string upc, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"album/upc:{upc}", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetAlbumComments(long albumId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"album/{albumId}/comments", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetAlbumFans(long albumId, int index = 0, int limit = 100, CancellationToken token = default) => await Call($"album/{albumId}/fans", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetAlbumTracks(long albumId, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"album/{albumId}/tracks", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetTrack(long trackId, CancellationToken token = default) => await Call($"track/{trackId}", token: token);

    public async Task<JToken> GetTrackByISRC(string isrc, CancellationToken token = default) => await Call($"track/isrc:{isrc}", token: token);

    public async Task<JToken> GetArtist(long artistId, CancellationToken token = default) => await Call($"artist/{artistId}", token: token);

    public async Task<JToken> GetArtistTop(long artistId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"artist/{artistId}/top", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistAlbums(long artistId, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"artist/{artistId}/albums", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistComments(long artistId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"artist/{artistId}/comments", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistFans(long artistId, int index = 0, int limit = 100, CancellationToken token = default) => await Call($"artist/{artistId}/fans", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistRelated(long artistId, int index = 0, int limit = 20, CancellationToken token = default) => await Call($"artist/{artistId}/related", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistRadio(long artistId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"artist/{artistId}/radio", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetArtistPlaylists(long artistId, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"artist/{artistId}/playlists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUser(long userId, CancellationToken token = default) => await Call($"user/{userId}", token: token);

    public async Task<JToken> GetUserAlbums(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/albums", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserArtists(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/artists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserFlow(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/flow", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserFollowing(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/followings", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserFollowers(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/followers", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserPlaylist(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/playlists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserRadios(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/radios", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetUserTracks(long userId, int index = 0, int limit = 25, CancellationToken token = default) => await Call($"user/{userId}/tracks", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChart(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChartTracks(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}/tracks", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChartAlbums(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}/albums", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChartArtists(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}/artists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChartPlaylists(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}/playlists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetChartPodcasts(long genreId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"chart/{genreId}/podcasts", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetComment(long commentId, CancellationToken token = default) => await Call($"comment/{commentId}", token: token);

    public async Task<JToken> GetEditorials(int index = 0, int limit = 10, CancellationToken token = default) => await Call($"editorial", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetEditorial(long genreId = 0, CancellationToken token = default) => await Call($"editorial/{genreId}", token: token);

    public async Task<JToken> GetEditorialSelection(long genreId = 0, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"editorial/{genreId}/selection", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetEditorialCharts(long genreId = 0, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"editorial/{genreId}/charts", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetEditorialReleases(long genreId = 0, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"editorial/{genreId}/releases", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetGenres(int index = 0, int limit = 10, CancellationToken token = default) => await Call($"genre", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetGenre(long genreId = 0, CancellationToken token = default) => await Call($"genre/{genreId}", token: token);

    public async Task<JToken> GetGenreArtists(long genreId = 0, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"genre/{genreId}/artists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetGenreRadios(long genreId = 0, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"genre/{genreId}/radios", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetInfos(CancellationToken token = default) => await Call($"infos", token: token);

    public async Task<JToken> GetOptions(CancellationToken token = default) => await Call($"options", token: token);

    public async Task<JToken> GetPlaylist(long playlistId, CancellationToken token = default) => await Call($"playlist/{playlistId}", token: token);

    public async Task<JToken> GetPlaylistComments(long playlistId, int index = 0, int limit = 10, CancellationToken token = default) => await Call($"playlist/{playlistId}/comments", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetPlaylistFans(long playlistId, int index = 0, int limit = 100, CancellationToken token = default) => await Call($"playlist/{playlistId}/fans", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetPlaylistTracks(long playlistId, int index = 0, int limit = -1, CancellationToken token = default) => await Call($"playlist/{playlistId}/tracks", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetPlaylistRadio(long playlistId, int index = 0, int limit = 100, CancellationToken token = default) => await Call($"playlist/{playlistId}/radio", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetRadios(int index = 0, int limit = 10, CancellationToken token = default) => await Call($"radio", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetRadiosGenres(int index = 0, int limit = 10, CancellationToken token = default) => await Call($"radio/genres", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetRadiosTop(int index = 0, int limit = 50, CancellationToken token = default) => await Call($"radio/top", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetRadiosLists(int index = 0, int limit = 25, CancellationToken token = default) => await Call($"radio/lists", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    public async Task<JToken> GetRadio(long radioId, CancellationToken token = default) => await Call($"radio/${radioId}", token: token);

    public async Task<JToken> GetRadioTracks(long radioId, int index = 0, int limit = 40, CancellationToken token = default) => await Call($"radio/${radioId}/tracks", new()
    {
        ["index"] = index.ToString(),
        ["limit"] = limit.ToString()
    }, token);

    private static string GenerateAdvancedSearchQuery(string? artist = null, string? album = null, string? track = null, string? label = null, int? durMin = null, int? durMax = null, int? bpmMin = null, int? bpmMax = null)
    {
        StringBuilder query = new();
        if (artist != null)
            query.Append($"artist:\"{artist}\" ");

        if (album != null)
            query.Append($"album:\"{album}\" ");

        if (track != null)
            query.Append($"track:\"{track}\" ");

        if (label != null)
            query.Append($"label:\"{label}\" ");

        if (durMin != null)
            query.Append($"dur_min:\"{durMin}\" ");

        if (durMax != null)
            query.Append($"dur_max:\"{durMax}\" ");

        if (bpmMin != null)
            query.Append($"bpm_min:\"{bpmMin}\" ");

        if (bpmMax != null)
            query.Append($"bpm_max:\"{bpmMax}\" ");

        return query.ToString().Trim();
    }

    private static Dictionary<string, string> GenerateSearchArgs(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25)
    {
        Dictionary<string, string> args = new()
        {
            ["q"] = query,
            ["index"] = index.ToString(),
            ["limit"] = limit.ToString()
        };

        if (strict)
            args.Add("strict", "on");

        if (order != null)
            args.Add("order", order.ToString()!);

        return args;
    }

    public async Task<JToken> Search(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchAdvanced(string? artist = null, string? album = null, string? track = null, string? label = null, int? durMin = null, int? durMax = null, int? bpmMin = null, int? bpmMax = null, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Search(PublicApi.GenerateAdvancedSearchQuery(artist, album, track, label, durMin, durMax, bpmMin, bpmMax), strict, order, index, limit, token);

    public async Task<JToken> SearchAlbum(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/album", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchArtist(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/artist", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchPlaylist(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/playlist", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchRadio(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/radio", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchTrack(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/track", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    public async Task<JToken> SearchUser(string query, bool strict = false, SearchOrder? order = null, int index = 0, int limit = 25, CancellationToken token = default)
        => await Call("search/user", PublicApi.GenerateSearchArgs(query, strict, order, index, limit), token);

    private async Task<JToken> Call(string method, Dictionary<string, string>? parameters = null, CancellationToken token = default)
    {
        parameters ??= [];

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
        HttpResponseMessage response = await _client.SendAsync(request, token);

        string resp = await response.Content.ReadAsStringAsync(token);
        JObject json = JObject.Parse(resp);

        JToken? error = json["error"];
        if (error != null && error.Any())
        {
            JToken? code = error["code"];
            if (code != null)
            {
                string? message = error["message"]?.ToString();
                int err = (int)code;
                switch (err)
                {
                    case 4:
                    case 700:
                        {
                            await Task.Delay(5000, token);
                            return await Call(method, parameters, token);
                        }
                    case 100:
                        throw new ItemsLimitExceededException(message ?? "");
                    case 200:
                        throw new PermissionException(message ?? "");
                    case 300:
                        throw new InvalidTokenException(message ?? "");
                    case 500:
                        throw new WrongParameterException(message ?? "");
                    case 501:
                        throw new MissingParameterException(message ?? "");
                    case 600:
                        throw new InvalidQueryException(message ?? "");
                    case 800:
                        throw new Exceptions.DataException(message ?? "");
                    case 901:
                        throw new IndividualAccountChangedNotAllowedException(message ?? "");
                }
            }

            throw new APIException(error.ToString());
        }

        return json;
    }
}