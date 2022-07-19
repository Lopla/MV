using System.CommandLine;

internal class CommandLineApplication
{
    public CommandLineApplication()
    {
    }

    public async Task<int> Main(params string[] args)
    {
        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console.");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        rootCommand.AddOption(fileOption);

        rootCommand.SetHandler((file) => 
            { 
                ReadFile(file!); 
            },
            fileOption);

        return await rootCommand.InvokeAsync(args);
    }

    private void ReadFile(object value)
    {
        throw new NotImplementedException();
    }
}