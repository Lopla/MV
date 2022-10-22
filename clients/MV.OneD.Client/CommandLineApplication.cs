using MV.OneD.Client.UI;
using System.CommandLine;

public static class CommandLineApplication
{
    public static async Task<int> MainCode(params string[] args)
    {
        var ideCommand = new Command("ide", "Start metaverse browser with Terminal GUI and debug capabilities.");
        ideCommand.SetHandler( async()=>{
            var app = new App();            
            await app.Start();    
        });

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
            await app.Start();
        }, optionAddress);

        var rootCommand = new RootCommand("MV");
        rootCommand.AddCommand(openCommand);
        rootCommand.AddCommand(ideCommand);
    
        return await rootCommand.InvokeAsync(args);
    }
}