using MV.Client;
using MV.Interfaces;
using Pla.Lib;
using Pla.Lib.UI;
using SkiaSharp;
using Button = Pla.Lib.UI.Button;

namespace MV.TwoD
{
    public class Ctx : IContext
    {
        public Ctx()
        {
        }

        private List<SKPoint> points = new List<SKPoint>();
        private IEngine e;
        private Manager manager;

        public IManifest Manifest { get; }

        public void Init(IEngine engine)
        {
            
            this.e = engine;
            this.manager = new Manager(engine);
            
            var b = new Button()
            {
                Bounds = new SKRect(10, 10, 30, 30),
                Label = "1",
            };
            
            b.ClickedHandler += (loc) =>
            {
                System.Console.WriteLine("a");
                this.e.RequestRefresh();
            };

            var number = new Edit();

            this.manager.Add(b);
        }

        public IPainter GetPainter()
        {
            return this.manager;
        }

        public IControl GetControl()
        {
            return this.manager;
        }
    }
}