using CommandLine;
using CommandLine.Text;

namespace Tsyrkov.Tgen;

public class CommandLineOptions
{
    [Option('t', "template", Required = true, HelpText = "Path to file with template.")]
    public string TemplateFilePath { get; set; } = string.Empty;

    [Option('p', "parameters", Required = true, HelpText = "Path to file with template parameters.")]
    public string TemplateParametersFilePath { get; set; } = string.Empty;
    
    [Option('o', "output", HelpText = "Path to output file. If file already exists, it will be overwritten.")]
    public string OutputFilePath { get; set; } = string.Empty;
        
    [Usage]
    public static IEnumerable<Example> Examples => new List<Example>
    {
        new ("Simple run", new CommandLineOptions
        {
            TemplateFilePath = "path/to/template.txt",
            TemplateParametersFilePath = "path/to/parameters.csv"
        }),
        new ("Save results to file", new CommandLineOptions
        {
            TemplateFilePath = "path/to/template.txt",
            TemplateParametersFilePath = "path/to/parameters.csv",
            OutputFilePath = "path/to/output.txt"
        })
    };
}