using System.CommandLine;
using System.CommandLine.Invocation;
using MV.OneD.UI;

public static class CommandLineApplication
{
    public static async Task<int> Main(params string[] args)
    {
        var optionAddress = new Option<int>(
            name: "--address",
            description: "Where to go after loading this app",
            getDefaultValue: () => 0
        );  

        var openCommand = new Command("open", "Start metaverse browser with Terminal GUI.")
        {
            optionAddress
        };     
        
        openCommand.SetHandler( async (address)=>{
            var app = new App();
            Console.WriteLine("start");
            await app.Start(address);
            Console.WriteLine("stop");
            
        }, optionAddress);


        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddCommand(openCommand);
    
        return await rootCommand.InvokeAsync(args);
    }


}