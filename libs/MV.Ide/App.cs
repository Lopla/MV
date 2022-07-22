using MV.Client;
using MV.Interfaces;
using Terminal.Gui;
using Pla.Lib.UI;

namespace MV.OneD.UI;

public static class Clients
{
    public static async Task StartOneD(IManifest manifest)
    {
        var metaVerseClient = new MVClient(new OneD.OneDConsole( ));
        await metaVerseClient.Init(manifest);
        await metaVerseClient.Start();        
    }
}