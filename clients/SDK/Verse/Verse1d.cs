using MV.Forms;
using MV.Interfaces;

namespace SDK.Verse
{
    public class Verse1d : IVerse
    {
        public async Task Start()
        {
            //1. create UI
            //2. ask to show it

            var dialer = new VFrame();
            for (var x = 0; x < 3; x++)
            {
                var row = new HFrame();
                for (var y = 0; y < 3; y++)
                {
                    var number = x + y * 3 + 1;
                    row.Add(new Button($"{number}"));
                }

                dialer.Add(row);
            }

            Context.Show(dialer);
        }

        public async Task Init(IMetaVerse context)
        {
            Context = context;
        }

        public IMetaVerse Context { get; set; }
    }
}
