using MV.Client;
using MV.Interfaces;
using MV.OneD;
using MV.TwoD;

namespace MV.IDE;

public static class Clients
{
    public static async Task StartOneD(IManifest manifest)
    {
        await Start(new OneDConsole(), manifest);
    }

    public static async Task StartTwoD(IManifest manifest)
    {
        await Start(new TwoDControl(), manifest);
    }

    public static async Task Start(IMetaVerse ctx, IManifest manifest)
    {
        var metaVerseClient = new MVClient(ctx, useFilesInsteadOfStream: true);
        await metaVerseClient.Init();
        await metaVerseClient.Start();
    }
}