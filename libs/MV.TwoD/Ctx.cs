using MV.Interfaces;
using Pla.Lib;
using Pla.Lib.UI;

namespace MV.TwoD
{
    public class Ctx : IContext
    {
        public Ctx()
        {
        }

        private IEngine e = null;

        public Manager manager;

        public IManifest Manifest { get; }

        public void Init(IEngine engine)
        {            
            this.e = engine;
            this.manager = new Manager(engine);
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