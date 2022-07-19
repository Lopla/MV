using System.CommandLine;
using System.CommandLine.Invocation;
using MV.IDE.Creator;
using MV.OneD.UI;

public static class CommandLineApplication
{
    public static async Task<int> MainCode(params string[] args)
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
        
        var createVerseCommand = new Command("verse", "Create sample verse file");
        createVerseCommand.SetHandler(()=>{
            var c = new Creator();
            Console.WriteLine(c.DefaultVerse());
        });
        var createCommand = new Command("create", "Allows to create some metaverse componenets"){
            createVerseCommand
        };

        openCommand.SetHandler( async (address)=>{
            var app = new App();            
            await app.Start(address);            
        }, optionAddress);


        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddCommand(openCommand);
        rootCommand.AddCommand(createVerseCommand);
    
        return await rootCommand.InvokeAsync(args);
    }


}