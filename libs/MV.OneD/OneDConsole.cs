using MV.Interfaces;
using Terminal.Gui;

namespace MV.OneD;

public class OneDConsole : IMetaVerseRunner
{
    private readonly TerminalRender _terminalRender;

    public OneDConsole()
    {
        
        _terminalRender = new TerminalRender();
    }

    public void Show(IElement element)
    {
        _terminalRender.Show(element);
    }

    public void Hide(IElement element)
    {
        
    }

    public void Update(IElement element)
    {
        
    }

    public async Task Start()
    {
        await _terminalRender.Start();
    }

    public Task Init()
    {
        return Task.CompletedTask;
    }

    public async Task InitVerse(IVerse verse)
    {
        await verse.Init(this);
        await verse.Start();
    }
}