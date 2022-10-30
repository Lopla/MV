using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace MV.TwoD
{
    internal class StartingVerse : IVerse2d<Skia2dEnviorment>
    {
        private IMetaVerseRunner? _ctx = null!;
        private readonly Label _statusLabel = new ();

        public Task InitEngine(Skia2dEnviorment env)
        {
            return Task.CompletedTask;
        }

        public Task Start()
        {
            var frame = new VFrame();
            frame.Add(new Label("MV:"));
            frame.Add(AddStartButton());
            frame.Add(AddExits());
            frame.Add(_statusLabel);
            _ctx!.Show(frame);
            
            this.Log("Started");

            return Task.CompletedTask;
        }

        private IElement AddExits()
        {
            var frame = new HFrame();
            return frame;
        }

        private void Log(string logLine)
        {
            this._statusLabel.Text = logLine;
            this._ctx!.Update(this._statusLabel);
        }
        
        private Button AddStartButton()
        {
            var b = new Button("Start");
            b.Clicked += () =>
            {
                _ctx!.Show(new Label("hmm"));
            };
            return b;
        }

        public Task Init(IMetaVerseRunner? context)
        {
            this._ctx = context;
            return Task.CompletedTask;
        }
    }
}
