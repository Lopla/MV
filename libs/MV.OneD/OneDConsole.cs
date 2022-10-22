using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD;

public class OneDConsole : IMetaVerse
{
    private readonly TerminalRender _terminalRender;

    public OneDConsole()
    {
        Application.Init();
        _terminalRender = new TerminalRender();
    }

    public void Show(IElement element)
    {
        // it always starts with the window
        var w = new Window
        {
            Width = Dim.Percent(50),
            Height = Dim.Percent(50)
        };

        _terminalRender.ShowV(w, element, (0, 0));

        Application.Top.Add(w);
    }

    public Task Start()
    {
        Application.Run();
        Application.Shutdown();

        return Task.CompletedTask;
    }

    public Task Init()
    {
        return Task.CompletedTask;
    }
}