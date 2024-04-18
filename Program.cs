using CommandLine;

namespace Tsyrkov.Tgen;

public static class Program
{
    public static void Main(string[] args)
    {
        var parser = new Parser(config => config.HelpWriter = Console.Out);
        
        parser.ParseArguments<CommandLineOptions>(args).WithParsed(Run);
    }
    
    private static void Run(CommandLineOptions options)
    {
        Logger log = new Logger();
        Core.Core core = new Core.Core(log);

        core.LoadTemplateFromFile(options.TemplateFilePath);
        core.LoadTableValuesFromCSV(options.ValuesFilePath);

        string result = core.RenderTemplate();
        Console.WriteLine("RESULT:");
        Console.Write(result);
    }
}
