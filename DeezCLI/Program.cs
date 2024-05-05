using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using DeezNET;
using DeezNET.Data;
using DeezNET.Exceptions;

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
            using SemaphoreSlim semaphore = new(Concurrent);
            foreach (long track in tracks)
            {
                await semaphore.WaitAsync();

                tasks.Add(Task.Run(async () =>
                {
                    await DoDownload(console, track);
                    semaphore.Release();
                }));
            }

            foreach (Task task in tasks)
            {
                await semaphore.WaitAsync();
                if (!task.IsCompleted) // i have a strong feeling adding this means ive done something wrong, but i have no idea honestly
                    task.Start();
            }

            await Task.WhenAll(tasks);
        }
        else
            throw new InvalidURLException("Failed to parse the given URL, unable to download.");

        console.Output.WriteLine("Download complete.");
    }

    private async Task DoDownload(IConsole console, long track)
    {
        byte[] trackData = await client.Downloader.GetRawTrackBytes(track, PreferredBitrate, Bitrate.MP3_320);
        if (Metadata)
        {
            trackData = await client.Downloader.ApplyMetadataToTrackBytes(track, trackData);
        }

        await File.WriteAllBytesAsync(Path.Combine(OutputDir, $"{track}.flac"), trackData);
    }
}