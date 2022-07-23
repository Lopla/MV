using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
using Pla.Lib.UI;
using Pla.Win;
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
        Show(elemnt, 10);
    }

    public int Show(IElement element, int offset)
    {
        if(element is Frame f)
        {
            int subOffset = offset;
            foreach(var e in f.Elements)
            {
                offset=this.Show(e.Value, offset);
                offset+=10;
            }
        }else if(element is Forms.Label lb)
        {
            this.ctx.manager.Add(new Pla.Lib.UI.Button(){
                Label = lb.Text,
                Bounds = new SkiaSharp.SKRect(10,offset, 110, offset+20)
            });
            offset+=20;
        } else if(element is Button bt)
        {
            this.ctx.manager.Add(new Pla.Lib.UI.Button(){
                Label = bt.Text,
                Bounds = new SkiaSharp.SKRect(10,offset, 110, offset+20)
            });
            offset+=20;
        }

        return offset;
    }

    public Task Start()
    {
        Application.Run(window);
        return Task.CompletedTask;
    }    
}
