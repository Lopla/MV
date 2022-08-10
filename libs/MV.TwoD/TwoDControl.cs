using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using Pla.Lib.UI;
using Pla.Win;
using SkiaSharp;
using Button = MV.Forms.Button;

namespace MV.TwoD;
public class TwoDControl : IMetaVerse
{
    Ctx ctx = new Ctx();
    private PlaWindow window;


    public TwoDControl()
    {

    }

    public Task Init()
    {
        this.window = new PlaWindow();
        window.Init(ctx);

        return Task.CompletedTask;
    }

    public void Show(IElement elemnt)
    {
        Show(elemnt, this.ctx.manager);
    }

    private void Show(IElement element, IWidgetContainer container)
    {
        // let's chcek our ui
        // and start size calculation from what we have

        int buttonHeight = 30;
        if (element is Forms.Frame f)
        {
            IWidgetContainer frame = null;
            if (f is VFrame || f is Forms.Frame)
            {
                frame = (IWidgetContainer)container.Add(new Pla.Lib.UI.Frame());
            }
            if(f is HFrame)
            {
                frame =(IWidgetContainer) container.Add(new Pla.Lib.UI.Frame(Pla.Lib.UI.FrameStyle.Horizontal));
            }

            foreach (var e in f.Elements)
            {
                this.Show(e.Value, frame);                
            }
        }
        else if (element is Forms.Label lb)
        {
            container.Add(new Pla.Lib.UI.Button()
            {
                Label = lb.Text,
            });
        }
        else if (element is Button bt)
        {
            container.Add(new Pla.Lib.UI.Button()
            {
                Label = bt.Text,
            });
        }
    }

    public Task Start()
    {
        Application.Run(window);
        return Task.CompletedTask;
    }
}
