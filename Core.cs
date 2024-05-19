using Microsoft.VisualBasic.FileIO;
using Serilog;

namespace Tsyrkov.Tgen;

public class Core
{
    private string Template { get; set; } = string.Empty;
    private List<string[]> TemplateParameters { get; set; } = [];

    public void LoadTemplateFromFile(string filePath)
    {
        try
        {
            Template = File.ReadAllText(filePath);
        }
        catch (Exception exception)
        {
            Log.Error("Template could not be loaded. Reason: {Message}", exception.Message);
        }
    }

    public void LoadTemplateParametersFromCsv(string filePath, string separator = ";")
    {
        try
        {
            using var textFieldParser = new TextFieldParser(filePath);

            textFieldParser.TextFieldType = FieldType.Delimited;
            textFieldParser.SetDelimiters(separator);

            while (!textFieldParser.EndOfData)
            {
                var rows = textFieldParser.ReadFields();

                if (rows != null)
                {
                    TemplateParameters.Add(rows);
                }
            }
        }
        catch (Exception exception)
        {
            Log.Error("Template parameters could not be loaded. Reason: {Message}", exception.Message);
        }
    }

    public string InstantiateTemplate()
    {
        var output = string.Empty;
        
        if (string.IsNullOrEmpty(Template))
        {
            Log.Warning("Template is not specified.");
        }
        
        if (TemplateParameters.Count == 0)
        {
            Log.Warning("Template parameters are not specified.");
        }

        for (var i = 0; i < TemplateParameters.Count; i++)
        {
            var templateInstance = new string(Template);

            for (var j = 0; j < TemplateParameters[i].Length; j++)
            {
                templateInstance = templateInstance.Replace($"@@{j}@@", TemplateParameters[i][j].Trim());
            }

            output += templateInstance;
            
            if (i < TemplateParameters.Count - 1)
            {
                output += Environment.NewLine;
            }
        }

        return output;
    }
}