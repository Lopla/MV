using MV.Client;
using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD.UI;

public class App
{
    public async Task Start(IManifest manifest)
    {
        Application.Init ();

        var metaVerseClient = new MVClient(new OneD.OneDConsole());
        await metaVerseClient.Init(manifest);

        var nav = new NavigcationMenu(metaVerseClient);
        nav.Setup(Application.Top);

        var label = new Label("Meta verse") 
        {
            X = Pos.Center(),
            Y = 0,
            Height = 1,
        };
 
        Application.Top.Add(label);
        Application.Run ();
        Application.Shutdown ();
    }
}