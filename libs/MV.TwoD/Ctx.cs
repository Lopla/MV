using Pla.Lib;
using Pla.Lib.UI;

namespace MV.TwoD;

public class Ctx : IContext
{
    private IEngine _engine;

    public Manager manager;
    
    public void Init(IEngine engine)
    {
        _engine = engine;
        manager = new Manager(engine);
    }

    public IPainter GetPainter()
    {
        return manager;
    }

    public IControl GetControl()
    {
        return manager;
    }
}