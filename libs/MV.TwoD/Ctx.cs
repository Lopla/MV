using Pla.Lib;
using Pla.Lib.UI;

namespace MV.TwoD;

public class Ctx : IContext
{
    private IEngine? _engine;

    public Manager Manager = null!;
    
    public void Init(IEngine engine)
    {
        _engine = engine;
        Manager = new Manager(engine);
    }

    public IPainter GetPainter()
    {
        return Manager;
    }

    public IControl GetControl()
    {
        return Manager;
    }
}