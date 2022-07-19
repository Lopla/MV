using System.CommandLine;
using System.CommandLine.Invocation;
using MV.OneD.UI;

public class CommandLineApplication
{
    public CommandLineApplication()
    {
    }

    public async Task<int> Main(params string[] args)
    {
        var rootCommand = new RootCommand("Start metaverse browser with Terminal GUI.");        
        rootCommand.SetHandler(this.OpenBrowser);
        return await rootCommand.InvokeAsync(args);
    }

    private async Task OpenBrowser(InvocationContext obj)
    {      
        var app = new App();

        await app.Start();
    }
}