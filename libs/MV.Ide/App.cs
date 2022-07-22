using MV.Client;
using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD.UI;

public class App
{
    public async Task Start(IManifest manifest)
    {
        Application.Init ();

        var metaVerseClient = new MVClient(new OneD.OneDConsole(Application.Top));
        await metaVerseClient.Init(manifest);

        await metaVerseClient.Start();
        
        Application.Run ();
        Application.Shutdown ();
    }
}