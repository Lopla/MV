using MV.Client;
using Terminal.Gui;

namespace MV.OneD.UI;

public class App
{
    public async Task Start(int? address)
    {
        Application.Init ();

        var metaVerseClient = new MVClient();
        await metaVerseClient.Init();

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