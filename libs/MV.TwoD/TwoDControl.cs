using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using MV.Skia;
using Pla.Lib.UI.Interfaces;
using Pla.Lib.UI.Widgets.Base;
using Pla.Win;
using Button = Pla.Lib.UI.Widgets.Button;
using FrameStyle = Pla.Lib.UI.Widgets.Enums.FrameStyle;
using Label = Pla.Lib.UI.Widgets.Label;

namespace MV.TwoD;

public class TwoDControl : IMetaVerseRunner
{
    private readonly Ctx _ctx = new();
    private readonly StartingVerse _startingVerse = new();
    private PlaWindow _window = null!;

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
        var el = FindWidgetByTag(element);
        if (el != null) el.Parent.Remove(el);
    }

    public void Update(IElement element)
    {
        var guiWidget = FindWidgetByTag(element);
        if (guiWidget != null)
            switch (guiWidget)
            {
                case Button b:
                    b.Text = (element as Forms.Button)?.Text;
                    break;
                case Label lb:
                    lb.Text = (element as Forms.Label)?.Text;
                    break;
            }
    }

    public async Task Start()
    {
        Application.Run(_window);
    }

    private Widget? FindWidgetByTag(IElement element, IWidgetContainer container = null)
    {
        if (container == null) return FindWidgetByTag(element, _ctx.Manager);

        if (container is Widget w)
            if (w.Tag == element)
                return w;

        foreach (var widget in container.Widgets)
        {
            if (widget is IWidgetContainer wc)
            {
                var foundWidget = FindWidgetByTag(element, wc);
                if (foundWidget != null)
                    return foundWidget;
            }

            if (widget.Tag == element) return widget;
        }

        return null;
    }

    private void Show(IElement element, IWidgetContainer container)
    {
        if (element is Frame f)
        {
            IWidgetContainer frame;
            if (f is VFrame)
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Widgets.Frame
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
        else if (element is Forms.Label lb)
        {
            container.Add(new Label
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