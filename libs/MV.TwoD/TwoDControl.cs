using MV.Interfaces;
using Pla.Lib.UI;

namespace MV.TwoD;
public class TwoDControl : IMetaVerse
{
    public TwoDControl()
    {
        
    }

    public Task Init()
    {
        return Task.CompletedTask;
    }

    public void Show(IElement form)
    {
        
    }

    public Task Start()
    {
        Pla.Win.App.PlaMain(new Ctx());
        return Task.CompletedTask;
    }
    
}
