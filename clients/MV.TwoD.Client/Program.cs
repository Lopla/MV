using Home;
using MV.Client;

namespace MV.TwoD.Client;

internal static class Program
{
    [STAThread]
    private static async Task Main()
    {
        ApplicationConfiguration.Initialize();
        
        var metaVerseClient = new MVClient(new TwoDControl());
        await metaVerseClient.Init();
        await metaVerseClient.Start();
    }
}