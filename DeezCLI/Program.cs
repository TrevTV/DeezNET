using DeezNET;

namespace DeezCLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var a = await DeezerClient.Create("", DeezNET.Data.Bitrate.FLAC);
            await a.DownloadToFile("", "");
        }
    }
}
