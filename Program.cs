using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

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
        try
        {
            var isOutputFileSpecified = !string.IsNullOrEmpty(options.OutputFilePath);
            
            PrepareLogger(suppressLogMessages: !isOutputFileSpecified);
            
            var core = new Core();

            core.LoadTemplateFromFile(options.TemplateFilePath);
            core.LoadTemplateParametersFromCsv(options.TemplateParametersFilePath);

            var output = core.InstantiateTemplate();

            if (isOutputFileSpecified)
            {
                File.WriteAllText(options.OutputFilePath, output);
                Log.Information("Results were saved in {OutputFilePath}", options.OutputFilePath);
            }
            else
            {
                Console.WriteLine(output);
            }
        }
        catch (Exception exception)
        {
            Log.Fatal("{Message}", exception.Message);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    
    private static void PrepareLogger(bool suppressLogMessages)
    {
        Log.Logger = new LoggerConfiguration()
                .Filter.ByExcluding(_ => suppressLogMessages)
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                        outputTemplate: "[{Level}] {Message:lj}{NewLine}{Exception}",
                        theme: AnsiConsoleTheme.Sixteen)
                .CreateLogger();
    }
}
