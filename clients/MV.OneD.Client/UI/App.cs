using Home;
using MV.Client;

namespace MV.OneD.Client.UI;

public class App
{
    public async Task Start()
    {
        var metaverseOneDContext = new OneDConsole();

        var metaVerseClient = new MVClient(metaverseOneDContext);
        await metaVerseClient.Init(new HomeManifest());
        await metaVerseClient.Start();
    }
}