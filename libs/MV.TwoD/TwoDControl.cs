using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using MV.Skia;
using Pla.Lib.UI.Interfaces;
using Pla.Lib.UI.Widgets.Base;
using Pla.Win;
using Button = Pla.Lib.UI.Widgets.Button;
using FrameStyle = Pla.Lib.UI.Widgets.Enums.FrameStyle;
using Label = MV.Forms.Label;

namespace MV.TwoD;

public class TwoDControl : IMetaVerseRunner
{
    private readonly Ctx _ctx = new();
    private PlaWindow _window = null!;
    private readonly StartingVerse _startingVerse = new();

    public Task Init()
    {
        _window = new PlaWindow();
        _window.Init(_ctx);

        return Task.CompletedTask;
    }

    public async Task InitVerse(IVerse verse)
    {
        await verse.Init(this);
        if (verse is IVerse2d<Skia2dEnviorment> s) await s.InitEngine(_ctx.Painter.Environment);

        await verse.Start();
    }

    public void Show(IElement element)
    {
        Show(element, _ctx.Manager);
    }

    public void Hide(IElement element)
    {
        var elmenet = this.FindWidgetByTag(element);
        if (elmenet != null)
        {
            
        }
    }

    private Widget? FindWidgetByTag(IElement element, IWidgetContainer container = null)
    {
        if (container == null)
        {
            return FindWidgetByTag(element, this._ctx.Manager);
        }

        if (container is Widget w)
        {
            if(w.Tag == element)
                return w;
        }

        foreach (var widget in container.Widgets)
        {
            if (widget is IWidgetContainer wc)
            {
                return FindWidgetByTag(element, wc);
            }

            if (widget.Tag == element)
            {
                return widget;
            }
        }

        return null;
    }

    public void Update(IElement element)
    {
        var guiWidget = this.FindWidgetByTag(element);
        if (guiWidget != null)
        {
            switch (guiWidget)
            {
                case Button b:
                    b.Text = (element as MV.Forms.Button)?.Text;
                    break;
                case Pla.Lib.UI.Widgets.Label lb:
                    lb.Text = (element as MV.Forms.Label)?.Text;
                    break;
                default:
                    break;
            }
        }
    }

    public async Task Start()
    {
        await InitVerse(_startingVerse);

        Application.Run(_window);
    }

    private void Show(IElement element, IWidgetContainer container)
    {
        if (element is Frame f)
        {
            IWidgetContainer frame;
            if (f is VFrame)
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Widgets.Frame()
                {
                    Tag = element
                });
            else //if (f is HFrame)
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Widgets.Frame(FrameStyle.Horizontal)
                {
                    Tag = element
                });

            foreach (var e in f.Elements) Show(e.Value, frame);
        }
        else if (element is Label lb)
        {
            container.Add(new Pla.Lib.UI.Widgets.Label()
            {
                Tag = element,
                Text = lb.Text
            });
        }
        else if (element is Forms.Button bt)
        {
            var btn = new Button
            {
                Tag = element,
                Text = bt.Text
            };
            btn.ClickedHandler += point => { bt?.OnClicked(); };
            container.Add(btn);
        }
    }
}