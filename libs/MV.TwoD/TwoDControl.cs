using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using MV.Skia;
using Pla.Lib.UI.Interfaces;
using Pla.Win;
using Button = Pla.Lib.UI.Widgets.Button;
using FrameStyle = Pla.Lib.UI.Widgets.Enums.FrameStyle;
using Label = MV.Forms.Label;

namespace MV.TwoD;

public class TwoDControl : IMetaVerseRunner
{
    private readonly Ctx _ctx = new();
    private PlaWindow _window = null!;
    private StartingVerse sw = new StartingVerse();

    public Task Init()
    {
        _window = new PlaWindow();
        _window.Init(_ctx);
        
        return Task.CompletedTask;
    }

    public async Task InitVerse(IVerse verse)
    {
        await verse.Init(this);
        if (verse is IVerse2d<Skia2dEnviorment> s)
        {
            await s.InitEngine(_ctx.Painter.Environment);
        }

        await verse.Start();
    }

    public void Show(IElement element)
    {
        Show(element, _ctx.Manager);
    }

    public async Task Start()
    {
        await this.InitVerse(sw);

        Application.Run(_window);
    }

    private void Show(IElement element, IWidgetContainer container)
    {
        if (element is Frame f)
        {
            IWidgetContainer frame;
            if (f is VFrame)
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Widgets.Frame());
            else //if (f is HFrame)
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Widgets.Frame(FrameStyle.Horizontal));

            foreach (var e in f.Elements) Show(e.Value, frame);
        }
        else if (element is Label lb)
        {
            container.Add(new Button
            {
                Text = lb.Text
            });
        }
        else if (element is Forms.Button bt)
        {
            var btn = new Button
            {
                Text = bt.Text,
            };
            btn.ClickedHandler += point =>
            {
                bt?.OnClicked();
            };
            container.Add(btn);
            
        }
    }
}