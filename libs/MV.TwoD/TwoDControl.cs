using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using Pla.Lib.UI.Interfaces;
using Pla.Win;
using Button = Pla.Lib.UI.Widgets.Button;
using FrameStyle = Pla.Lib.UI.Widgets.Enums.FrameStyle;
using Label = MV.Forms.Label;

namespace MV.TwoD;

public class TwoDControl : IMetaVerse
{
    private readonly Ctx _ctx = new();
    private PlaWindow _window = null!;
    
    public Task Init()
    {
        _window = new PlaWindow();
        _window.Init(_ctx);

        return Task.CompletedTask;
    }

    public void Show(IElement element)
    {
        Show(element, _ctx.Manager);
    }

    public Task Start()
    {
        Application.Run(_window);
        return Task.CompletedTask;
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
            container.Add(new Button
            {
                Text = bt.Text
            });
        }
    }
}