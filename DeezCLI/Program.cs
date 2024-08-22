using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using DeezNET;
using DeezNET.Data;
using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using System.Text;

namespace DeezCLI;

internal class Program
{
    private static async Task<int> Main() =>
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
}

[Command(Description = "Downloads the given URL.")]
public class DownloadCommand : ICommand
{
    [CommandParameter(0, Description = "The URL of the item to download. Can be a shortened URL.")]
    public required string URL { get; init; }

    [CommandOption("bitrate", 'b', Description = "The preferred bitrate when downloading. Falls back to a lower quality if unavailable.")]
    public Bitrate PreferredBitrate { get; init; } = Bitrate.FLAC;

    [CommandOption("add-metadata", 'm', Description = "Whether to attach metadata to the downloaded audio file.")]
    public bool Metadata { get; init; } = true;

    [CommandOption("add-sync-lyrics", 'L', Description = "Specifies whether to add a synced lyrics file to the download directory.")]
    public bool SyncLyrics { get; init; } = false;

    [CommandOption("add-lrclib-lyrics", 'B', Description = "Specifies whether to fetch lyrics from LRCLIB if available.")]
    public bool LrcLibLyrics { get; init; } = false;

    [CommandOption("output", 'o', Description = "The directory to save downloaded media to.")]
    public string OutputDir { get; init; } = Environment.CurrentDirectory;

    [CommandOption("arl", 'a', Description = "The account ARL to download with. A paid plan allows for higher quality downloads. Some regions do not have full tracks available without a premium account.")]
    public string ARL { get; init; } = "";

    [CommandOption("top-limit", 'l', Description = "The max amount of tracks to download. Only applicable when downloading an artist's top tracks.")]
    public int TopLimit { get; init; } = 100;

    [CommandOption("concurrent", 'c', Description = "The max amount of allowed concurrent track downloads.")]
    public int Concurrent { get; init; } = 3;

    [CommandOption("folder-template", 'd', Description = "The folder path template to use when saving tracks to file.")]
    public string FolderTemplate { get; init; } = "%albumartist%/%album%/";

    [CommandOption("file-template", 'f', Description = "The file path template to use when saving tracks to file.")]
    public string FileTemplate { get; init; } = "%track% - %title%.%ext%";

    public async ValueTask ExecuteAsync(IConsole console)
    {
        IAnsiConsole ansiConsole = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Ansi = AnsiSupport.Detect,
            ColorSystem = ColorSystemSupport.Detect,
            Out = new AnsiConsoleOutput(console.Output)
        });

        DeezerClient client = new();
        await client.SetARL(ARL);

        if (DeezerURL.TryParse(URL, out DeezerURL parsedUrl))
        {
            long[] tracks = await parsedUrl.GetAssociatedTracks(client, TopLimit);

            List<Task> tasks = [];
            using SemaphoreSlim semaphore = new(Concurrent, Concurrent);
            foreach (long track in tracks)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await DoDownload(ansiConsole, client, track);
                    }
                    catch (Exception ex)
                    {
                        ansiConsole.MarkupLine("[red]Unhandled exception occurred when downloading track {0}.[/]", track);
                        ansiConsole.WriteException(ex);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
        else
            throw new InvalidURLException("Failed to parse the given URL, unable to download.");

        ansiConsole.Markup("[green]Download complete.[/]");
    }

    private async Task DoDownload(IAnsiConsole console, DeezerClient client, long track)
    {
        JToken page = await client.GWApi.GetTrackPage(track);
        JToken albumPage = await client.GWApi.GetAlbumPage(page["DATA"]!["ALB_ID"]!.Value<long>());
        string songTitle = page["DATA"]!["SNG_TITLE"]!.ToString();
        string artistName = page["DATA"]!["ART_NAME"]!.ToString();
        string albumTitle = page["DATA"]!["ALB_TITLE"]!.ToString();
        int duration = page["DATA"]!["DURATION"]!.Value<int>();

        console.Write(new Rule($"[yellow]Track[/] {track} [yellow][/]").Justify(Justify.Center));
        console.MarkupLine($"[bold yellow]TITLE:[/] {songTitle}");
        console.MarkupLine($"[bold yellow]ARTIST:[/] {artistName}");
        console.MarkupLine($"[bold yellow]ALBUM:[/] {albumTitle}");
        console.Write(new Rule());

        string outPath = Path.Combine(OutputDir, GetFilledTemplate(FolderTemplate, page, albumPage), GetFilledTemplate(FileTemplate, page, albumPage));
        string outDir = Path.GetDirectoryName(outPath)!;
        if (!Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        await client.Downloader.WriteRawTrackToFile(track, outPath, PreferredBitrate, Downloader.GetLowerFallbackBitrate(PreferredBitrate));

        string plainLyrics = string.Empty;
        List<SyncLyrics>? syncLyrics = null;

        var lyrics = await client.Downloader.FetchLyricsFromDeezer(track);
        if (lyrics.HasValue)
        {
            plainLyrics = lyrics.Value.plainLyrics;

            if (SyncLyrics)
                syncLyrics = lyrics.Value.syncLyrics;
        }

        if (LrcLibLyrics && (string.IsNullOrWhiteSpace(plainLyrics) || (SyncLyrics && !(syncLyrics?.Any() ?? false))))
        {
            lyrics = await client.Downloader.FetchLyricsFromLRCLIB("lrclib.net", songTitle, artistName, albumTitle, duration);
            if (lyrics.HasValue)
            {
                if (string.IsNullOrWhiteSpace(plainLyrics))
                    plainLyrics = lyrics.Value.plainLyrics;
                if (SyncLyrics && !(syncLyrics?.Any() ?? false))
                    syncLyrics = lyrics.Value.syncLyrics;
            }
        }

        if (string.IsNullOrWhiteSpace(plainLyrics))
            console.MarkupLine($"[yellow]No plain lyrics for track {songTitle} ({track}) were available.[/]");

        if (!(syncLyrics?.Any() ?? false))
            console.MarkupLine($"[yellow]No synced lyrics for track {songTitle} ({track}) were available.[/]");

        if (Metadata)
             await client.Downloader.ApplyMetadataToFile(track, outPath, 512, plainLyrics);

        try
        {
            string artOut = Path.Combine(outDir, "folder.jpg");
            if (!File.Exists(artOut))
            {
                byte[] bigArt = await client.Downloader.GetArtBytes(page["DATA"]!["ALB_PICTURE"]!.ToString(), 1024);
                await File.WriteAllBytesAsync(artOut, bigArt);
            }
        }
        catch (UnavailableArtException)
        {
            console.MarkupLine($"[yellow]Artwork for track {songTitle} ({track}) was unavailable.[/]");
        }

        if (syncLyrics != null)
            await CreateLrcFile(Path.Combine(outDir, GetFilledTemplate(FileTemplate, page, albumPage, true)), syncLyrics);

        console.MarkupLine($"[yellow][[#]] Track[/] {track} [yellow]downloaded.[/]");
    }

    private string GetFilledTemplate(string template, JToken page, JToken albumPage, bool lrc = false)
    {
        StringBuilder t = new(template);
        ReplaceC("%title%", page["DATA"]!["SNG_TITLE"]!.ToString());
        ReplaceC("%album%", page["DATA"]!["ALB_TITLE"]!.ToString());
        ReplaceC("%albumartist%", albumPage["DATA"]!["ART_NAME"]!.ToString());
        ReplaceC("%artist%", page["DATA"]!["ART_NAME"]!.ToString());
        ReplaceC("%albumartists%", string.Join("; ", albumPage["DATA"]!["ARTISTS"]!.Select(a => a["ART_NAME"]!.ToString())));
        ReplaceC("%artists%", string.Join("; ", page["DATA"]!["ARTISTS"]!.Select(a => a["ART_NAME"]!.ToString())));
        ReplaceC("%track%", $"{(int)page["DATA"]!["TRACK_NUMBER"]!:00}");
        ReplaceC("%trackcount%", albumPage["SONGS"]!["total"]!.ToString());
        ReplaceC("%trackid%", page["DATA"]!["SNG_ID"]!.ToString());
        ReplaceC("%albumid%", page["DATA"]!["ALB_ID"]!.ToString());
        ReplaceC("%artistid%", page["DATA"]!["ART_ID"]!.ToString());

        if (!lrc)
            t.Replace("%ext%", PreferredBitrate == Bitrate.FLAC ? "flac" : "mp3");
        else
            t.Replace("%ext%", "lrc");
        DateTime releaseDate = DateTime.Parse(page["DATA"]!["PHYSICAL_RELEASE_DATE"]!.ToString());
        t.Replace("%year%", releaseDate.Year.ToString());
        return t.ToString();

        void ReplaceC(string o, string r)
        {
            t.Replace(o, CleanPath(r));
        }
    }

    /// <summary>
    /// Creates an LRC file for synchronized lyrics.
    /// </summary>
    /// <param name="lrcFilePath">The path to save the LRC file.</param>
    /// <param name="syncLyrics">The list of synchronized lyrics.</param>
    public async Task CreateLrcFile(string lrcFilePath, List<SyncLyrics> syncLyrics)
    {
        StringBuilder lrcContent = new();
        foreach (var lyric in syncLyrics)
        {
            if (!string.IsNullOrEmpty(lyric.LrcTimestamp) && !string.IsNullOrEmpty(lyric.Line))
            {
                lrcContent.AppendLine($"{lyric.LrcTimestamp} {lyric.Line}");
            }
        }
        await File.WriteAllTextAsync(lrcFilePath, lrcContent.ToString());
    }

    private static string CleanPath(string str)
    {
        char[] invalid = Path.GetInvalidFileNameChars();
        for (int i = 0; i < invalid.Length; i++)
        {
            char c = invalid[i];
            str = str.Replace(c, '_');
        }
        return str;
    }
}