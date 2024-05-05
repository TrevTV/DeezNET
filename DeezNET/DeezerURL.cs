using DeezNET.Data;
using DeezNET.Exceptions;
using System.Text.RegularExpressions;

namespace DeezNET;

/// <summary>
/// A representation of a Deezer URL for usage with the API.
/// </summary>
/// <param name="url">The complete Deezer URL, if the original URL is shortened, this will be the full, unshortened link.</param>
/// <param name="type">The entity type this URL represents.</param>
/// <param name="id">The entity ID this URL references.</param>
public class DeezerURL(string url, EntityType type, long id)
{
    /// <summary>
    /// The complete Deezer URL, if the original URL is shortened, this will be the full, unshortened link.
    /// </summary>
    public string Url { get; init; } = url;
    /// <summary>
    /// The entity type this URL represents.
    /// </summary>
    public EntityType EntityType { get; init; } = type;
    /// <summary>
    /// The entity ID this URL references.
    /// </summary>
    public long Id { get; init; } = id;

    private static HttpClient _httpClient = new(new HttpClientHandler()
    {
        AllowAutoRedirect = false,
    });

    /// <summary>
    /// Parses a URL.
    /// </summary>
    /// <param name="url">The URL to parse.</param>
    /// <param name="deezerUrl">The parsed DeezerURL.</param>
    /// <returns>If the parsing was successful.</returns>
    public static bool TryParse(string url, out DeezerURL deezerUrl)
    {
        try
        {
            deezerUrl = Parse(url);
            return true;
        }
        catch
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            deezerUrl = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return false;
        }
    }

    /// <summary>
    /// Parses a URL.
    /// </summary>
    /// <param name="url">The URL to parse.</param>
    /// <returns>The parsed DeezerURL.</returns>
    /// <exception cref="InvalidURLException"></exception>
    public static DeezerURL Parse(string url)
    {
        if (url.Contains("deezer.page.link"))
            url = UnshortenURL(url);

        int paramStart = url.IndexOf('?');
        if (paramStart != -1)
            url = url[..paramStart];

        // deemix handles discography "separately" from normal items, but it's effectively the same exact thing, so we aren't going to act like they're different
        int discoStart = url.IndexOf("/discography");
        if (discoStart != -1)
            url = url[..discoStart];

        EntityType type;
        string? rawId;
        if (url.Contains("/track/"))
        {
            type = EntityType.Track;
            rawId = Regex.Match(url, "/track/(.+)").Groups[1].Value;
        }
        else if (url.Contains("/playlist/"))
        {
            type = EntityType.Playlist;
            rawId = Regex.Match(url, "/playlist/(\\d+)").Groups[1].Value;
        }
        else if (url.Contains("/album/"))
        {
            type = EntityType.Album;
            rawId = Regex.Match(url, "/album/(.+)").Groups[1].Value;
        }
        else if (url.Contains("/artist/") && url.Contains("/top_track"))
        {
            type = EntityType.ArtistTop;
            rawId = Regex.Match(url, "/artist/(\\d+)/top_track").Groups[1].Value;
        }
        else if (url.Contains("/artist"))
        {
            type = EntityType.Artist;
            rawId = Regex.Match(url, "/artist/(\\d+)").Groups[1].Value;
        }
        else
            throw new InvalidURLException($"Unable to determine type of URL \"{url}\".");

        if (long.TryParse(rawId, out long id))
            return new DeezerURL(url, type, id);
        else
            throw new InvalidURLException($"URL \"{url}\" has an un-parseable entity ID.");
    }

    /// <summary>
    /// Finds the redirect of a shortened URL.
    /// </summary>
    /// <param name="url">The shortened URL.</param>
    /// <returns>The unshortened URL. Include any additional parameters, such as tracking.</returns>
    /// <exception cref="InvalidURLException"></exception>
    public static string UnshortenURL(string url)
    {
        HttpRequestMessage req = new(HttpMethod.Get, url);
        HttpResponseMessage resp = _httpClient.Send(req);

        string? target = resp.StatusCode == System.Net.HttpStatusCode.Redirect ? resp.Headers.Location?.OriginalString : null;
        if (target == null)
            throw new InvalidURLException($"{url} did not provide a redirect Location header.");

        return target!;
    }

    /// <summary>
    /// Gets the tracks associated with the entity.
    /// </summary>
    /// <param name="client">The DeezerClient to use when contacting the API. No ARL is necessary.</param>
    /// <param name="topLimit">The max amount of tracks to return. Only applicable when EntityType is ArtistTop.</param>
    /// <returns></returns>
    public async Task<long[]> GetAssociatedTracks(DeezerClient client, int topLimit = 100)
    {
        switch (EntityType)
        {
            case EntityType.Track:
                return [Id];
            case EntityType.Playlist:
                return (await client.PublicApi.GetPlaylistTracks(Id))["data"]!.Select(t => (long)t["id"]!).ToArray();
            case EntityType.Album:
                return (await client.PublicApi.GetAlbumTracks(Id))["data"]!.Select(t => (long)t["id"]!).ToArray();
            case EntityType.ArtistTop:
                return (await client.PublicApi.GetArtistTop(Id, 0, topLimit))["data"]!.Select(t => (long)t["id"]!).ToArray();
            case EntityType.Artist:
                {
                    long[] albumIds = (await client.PublicApi.GetArtistAlbums(Id))["data"]!.Select(a => (long)a["id"]!).ToArray();
                    List<long> trackIds = [];
                    for (int i = 0; i < albumIds.Length; i++)
                        trackIds.AddRange((await client.PublicApi.GetAlbumTracks(Id))["data"]!.Select(t => (long)t["id"]!));
                    return trackIds.ToArray();
                }
        }

        return [];
    }
}
