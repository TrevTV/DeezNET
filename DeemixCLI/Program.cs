using Deemix;

namespace DeemixCLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var a = await DeemixClient.Create("");
            await a.Download("", "");
        }
    }
}
