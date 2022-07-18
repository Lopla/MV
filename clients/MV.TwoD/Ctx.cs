using Pla.Lib;
using Pla.Lib.UI;
using SkiaSharp;
using Button = Pla.Lib.UI.Button;

namespace MV.TwoD
{
    internal class Ctx : IContext
    {
        private List<SKPoint> points = new List<SKPoint>();
        private IEngine e;
        private Manager manager;
        private Logic logic;

        public void Init(IEngine engine)
        {
            this.e = engine;
            this.manager = new Manager(engine);

            this.logic = new Logic();

            var b = new Button()
            {
                Bounds = new SKRect(10, 100, 100, 130),
                Label = "Click me",
            };
            b.ClickedHandler += (loc) =>
            {
                System.Console.WriteLine("a");
                this.e.RequestRefresh();
            };

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