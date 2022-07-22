using MV.Client;
using MV.Interfaces;
using Terminal.Gui;
using Pla.Lib.UI;

namespace MV.OneD.UI;

public static class Clients
{
    public static async Task StartOneD(IManifest manifest)
    {
        await Start(new OneD.OneDConsole( ), manifest);
    }

    public static async Task StartTwoD(IManifest manifest)
    {
        await Start(new TwoD.TwoDControl(), manifest);
    }

    public static async Task Start(IMetaVerse ctx, IManifest manifest)
    {
        var metaVerseClient = new MVClient(ctx);
        await metaVerseClient.Init(manifest);
        await metaVerseClient.Start();        
    }
}