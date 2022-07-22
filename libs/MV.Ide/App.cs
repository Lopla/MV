using MV.Client;
using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD.UI;

public class App
{
    public async Task Start(IManifest manifest)
    {
        var metaVerseClient = new MVClient(new OneD.OneDConsole( ));
        await metaVerseClient.Init(manifest);
        await metaVerseClient.Start();        
    }
}