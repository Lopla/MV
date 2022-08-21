using MV.Client;
using Terminal.Gui;

namespace MV.OneD.Client.UI;

public class App
{
    public async Task Start()
    {
        var metaverseOneDContext = new OneDConsole();

        var metaVerseClient = new MVClient(metaverseOneDContext);
        await metaVerseClient.Init(new Home.HomeManifest());
        await metaVerseClient.Start();
    }
}