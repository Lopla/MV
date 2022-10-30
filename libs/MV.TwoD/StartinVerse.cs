using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace MV.TwoD
{
    internal class StartingVerse : IVerse2d<Skia2dEnviorment>
    {
        private IMetaVerseRunner? _ctx = null!;
        private readonly HFrame _frame = new HFrame();
        private readonly Label _statusLabel = new Label();

        public Task InitEngine(Skia2dEnviorment env)
        {
            return Task.CompletedTask;
        }

        public Task Start()
        {
            _frame.Add(new Label("MV"));
            _frame.Add(AddStartButton());
            _frame.Add(_statusLabel);
            
            ////show all entries

            _ctx!.Show(_frame);
            
            this.Log("Started");

            return Task.CompletedTask;
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
