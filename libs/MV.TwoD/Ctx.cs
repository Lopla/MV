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
        public Ctx(IManifest manifest)
        {
            Manifest = manifest;
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
 
            this.InitAndRunMV().Wait();
        }


        private async Task InitAndRunMV()
        {
            var metaverseOneDContext = new TwoDControl(this.manager);
            var metaVerseClient = new MVClient(metaverseOneDContext);
            await metaVerseClient.Init(Manifest);
            await metaVerseClient.Start();
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