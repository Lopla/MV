using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace MV.TwoD
{
    internal class StartingVerse : IVerse2d<Skia2dEnviorment>
    {
        private IMetaVerseRunner? _ctx = null!;
        private readonly Label _statusLabel = new ();
        private readonly VFrame _frame = new VFrame();
        private bool _collapsed = false;

        public Task InitEngine(Skia2dEnviorment env)
        {
            return Task.CompletedTask;
        }

        public Task Start()
        {
            _ctx!.Show(AddHamburgerButton());
            if (!_collapsed)
            {
                _frame.Add(new Label("MV:"));
                _frame.Add(AddStartButton());
                _frame.Add(AddExits());
                _frame.Add(_statusLabel);
                _ctx!.Show(_frame);
            }

            this.Log("Started");

            return Task.CompletedTask;
        }

        private IElement AddHamburgerButton()
        {
            var b = new Button()
            {
                Text = "="
            };
            
            b.Clicked += () =>
            {
                _collapsed = !_collapsed;
                if (_collapsed)
                {
                    this._ctx!.Hide(_frame);
                }
                else
                {
                    this._ctx!.Show(_frame);
                }
                
            };

            return b;
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
