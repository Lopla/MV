using Pla.Lib;
using Pla.Lib.UI;
using Pla.Lib.UI.DrawingStyles.Ami;

namespace MV.TwoD;

public class Ctx : IContext
{
    private IEngine? _engine;
    public Manager Manager = null!;
    public ExtendedPainter Painter = null!;

    public void Init(IEngine engine)
    {
        _engine = engine;
        Manager = new Manager(engine, new AmiMagic());
        Painter = new ExtendedPainter(Manager);
    }

    public IPainter GetPainter()
    {
        return Painter;
    }

    public IControl GetControl()
    {
        return Manager;
    }
}