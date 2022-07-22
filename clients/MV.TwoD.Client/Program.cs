using MV.Client;
using Pla.Lib.UI;

namespace MV.TwoD.Client;

static class Program
{
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();


        var manifest = new Home.HomeManifest();

        var metaVerseClient = new MVClient(new TwoDControl());
        await metaVerseClient.Init(manifest);
        await metaVerseClient.Start();
    }    
}

