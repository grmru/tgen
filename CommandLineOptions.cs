using CommandLine;
using CommandLine.Text;

namespace Tsyrkov.Tgen;

public class CommandLineOptions
{
    [Option('t', "template", Required = true, HelpText = "Path to file with template.")]
    public string TemplateFilePath { get; set; }

    [Option('v', "values", Required = true, HelpText = "Path to file with values.")]
    public string ValuesFilePath { get; set; }
    
    [Option('o', "output", HelpText = "Output file name.")]
    public string OutputFileName { get; set; }
        
    [Usage]
    public static IEnumerable<Example> Examples => new List<Example>
    {
        new ("Simple run", new CommandLineOptions
        {
            TemplateFilePath = "path/to/template.txt",
            ValuesFilePath = "path/to/values.csv"
        }),
        new ("Save results to file", new CommandLineOptions
        {
            TemplateFilePath = "path/to/template.txt",
            ValuesFilePath = "path/to/values.csv",
            OutputFileName = "output.txt"
        })
    };
}