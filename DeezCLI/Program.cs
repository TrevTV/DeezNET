using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using DeezNET;
using DeezNET.Data;
using DeezNET.Exceptions;
using Newtonsoft.Json.Linq;

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

    [CommandOption("bitrate", 'b', Description = "The preferred bitrate when downloading. Fallbacks if unavailable.")]
    public Bitrate PreferredBitrate { get; init; } = Bitrate.FLAC;

    [CommandOption("add-metadata", 'm', Description = "Whether to attach metadata to the downloaded audio file.")]
    public bool Metadata { get; init; } = true;

    [CommandOption("output", 'o', Description = "The directory to save downloaded media to.")]
    public string OutputDir { get; init; } = Environment.CurrentDirectory;

    [CommandOption("arl", 'a', Description = "The account ARL to download with. A paid plan allows for higher quality downloads. Some regions do not have full tracks available without a premium account.")]
    public string ARL { get; init; } = "";

    [CommandOption("top-limit", 'l', Description = "The max amount of tracks to download. Only applicable when downloading an artist's top tracks.")]
    public int TopLimit { get; init; } = 100;

    [CommandOption("concurrent", 'c', Description = "The max amount of allowed concurrent track downloads.")]
    public int Concurrent { get; init; } = 3;

    private DeezerClient client;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        client = await DeezerClient.Create(ARL);
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
                        await DoDownload(console, track);
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

        console.Output.WriteLine("Download complete.");
    }

    private async Task DoDownload(IConsole console, long track)
    {
        console.Output.WriteLine($"Downloading track {track}...");

        JToken page = await client.GWApi.GetTrackPage(track);
        byte[] trackData = await client.Downloader.GetRawTrackBytes(track, PreferredBitrate, Bitrate.MP3_320); // TODO: improve fallback
        if (Metadata)
        {
            trackData = await client.Downloader.ApplyMetadataToTrackBytes(track, trackData);
        }

        // TODO: could use variables like deemixgui to setup custom export pathing

        string artistFolderPath = Path.Combine(OutputDir, CleanPath(page["DATA"]!["ART_NAME"]!.ToString()));
        if (!Directory.Exists(artistFolderPath))
            Directory.CreateDirectory(artistFolderPath);

        string albumFolderPath = Path.Combine(artistFolderPath, CleanPath(page["DATA"]!["ALB_TITLE"]!.ToString()));
        if (!Directory.Exists(albumFolderPath))
            Directory.CreateDirectory(albumFolderPath);

        string title = CleanPath(page["DATA"]!["SNG_TITLE"]!.ToString());
        int trackNum = int.Parse(page["DATA"]!["TRACK_NUMBER"]!.ToString());
        string ext = PreferredBitrate == Bitrate.FLAC ? "flac" : "mp3";
        string outPath = Path.Combine(albumFolderPath, $"{trackNum:00} - {title}.{ext}");

        await File.WriteAllBytesAsync(outPath, trackData);

        console.Output.WriteLine($"Finished track {track}...");
    }

    private string CleanPath(string str)
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