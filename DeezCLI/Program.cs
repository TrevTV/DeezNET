using DeezNET;

namespace DeezCLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cli = await DeezerClient.Create("");
            var bytes = await cli.Downloader.GetTrackBytes(401934352, DeezNET.Data.Bitrate.FLAC);
            File.WriteAllBytes(@"C:\Users\trevo\Desktop\out.flac", bytes);
        }
    }
}
