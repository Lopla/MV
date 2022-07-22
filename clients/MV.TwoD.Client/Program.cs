using Pla.Lib.UI;

namespace MV.TwoD.Client;

static class Program
{
    [STAThread]
    static void Main()
    {

        ApplicationConfiguration.Initialize();
        Pla.Win.App.PlaMain(new Ctx(new Home.HomeManifest()));
    }    
}