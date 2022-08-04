using System.Windows.Forms;
using MV.Forms;
using MV.Interfaces;
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
        Show(elemnt, new Offest());
    }

    private void Show(IElement element, Offest offset)
    {
        int buttonHeight = 30;
        if (element is Frame f)
        {
            offset.Push();
            if(f is VFrame || f is Frame)
            {
                offset.SetDirectionV();
            }
            if(f is HFrame)
            {
                offset.SetDirectionH();
            }

            foreach (var e in f.Elements)
            {
                this.Show(e.Value, offset);
                offset.AddMargin();
            }
            offset.Pop();
        }
        else if (element is Forms.Label lb)
        {
            this.ctx.manager.Add(new Pla.Lib.UI.Button()
            {
                Label = lb.Text,
                Bounds = offset.GetRect(110, buttonHeight)
            });
            offset.AddMargin();
        }
        else if (element is Button bt)
        {
            this.ctx.manager.Add(new Pla.Lib.UI.Button()
            {
                Label = bt.Text,
                Bounds = offset.GetRect(110, buttonHeight)
            });
            offset.AddMargin();
        }
    }

    private class Offest : ICloneable
    {
        SkiaSharp.SKPoint Position = new SkiaSharp.SKPoint(0, 0);
        SkiaSharp.SKPoint Direction = new SkiaSharp.SKPoint(0, 1);
        SkiaSharp.SKPoint Margin = new SkiaSharp.SKPoint(10, 10);

        public void NewFrame()
        {
            Position.X = 0;
        }

        public void SetDirectionV()
        {
            Direction = new SkiaSharp.SKPoint(0, 1);
        }
        
        public void SetDirectionH()
        {
            Direction = new SkiaSharp.SKPoint(1, 0);
        }
        public void AddMargin()
        {
            this.Position.Offset(Direction.X * Margin.X, Direction.Y * Margin.Y);
        }

        public SKRect GetRect(float w, float h)
        {
            var newPosition = Position;
            newPosition.Offset(w, h);

            var result = new SKRect(Position.X, Position.Y, newPosition.X, newPosition.Y);
           
            float dx = Direction.X * w;
            float dy = Direction.Y * h;
            this.Position.Offset(dx, dy);

            return result;
        }

        Stack<Offest> stack = new Stack<Offest>();

        internal void Push()
        {
            stack.Push((Offest)this.Clone());
        }

        internal void Pop()
        {
            var current = (Offest)this.Clone();
            var o = this.stack.Pop();
            this.Direction = o.Direction;
            this.Position = o.Position;
            this.Margin = o.Margin;

            var offsetFromPrevious = current.Position;
            var delta = offsetFromPrevious - this.Position;
            
            float dx = Direction.X * delta.X;
            float dy = Direction.Y * delta.Y;
            this.Position.Offset(dx, dy);

        }

        public object Clone()
        {
            return new Offest()
            {
                Direction = new SKPoint(this.Direction.X, this.Direction.Y),
                Margin = new SKPoint(this.Margin.X, this.Margin.Y),
                Position = new SKPoint(this.Position.X, this.Position.Y),
            };
        }
    }

    public Task Start()
    {
        Application.Run(window);
        return Task.CompletedTask;
    }
}
