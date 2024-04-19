using Microsoft.VisualBasic.FileIO;
using Serilog;

namespace Tsyrkov.Tgen;

public class Core
{
    private string Template { get; set; } = string.Empty;
    private List<string[]> TableValues { get; set; } = [];

    public void LoadTemplateFromFile(string filePath)
    {
        try
        {
            Template = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(Template))
            {
                Log.Warning("Template is empty.");
            }
        }
        catch (Exception exception)
        {
            Log.Error("Template could not be loaded. Reason: {Message}", exception.Message);
        }
    }

    public void LoadTableValuesFromCsv(string filePath, string separator = ";")
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
                    TableValues.Add(rows);
                }
            }
        }
        catch (Exception exception)
        {
            Log.Error("Values could not be loaded. Reason: {Message}", exception.Message);
        }
    }

    public string FillTemplate()
    {
        var output = string.Empty;
        
        if (TableValues.Count == 0)
        {
            Log.Warning("There are no values.");
            return output;
        }

        for (var i = 0; i < TableValues.Count; i++)
        {
            var currentPattern = new string(Template);

            for (var j = 0; j < TableValues[i].Length; j++)
            {
                currentPattern = currentPattern.Replace($"@@{j}@@", TableValues[i][j].Trim());
            }

            output += currentPattern;
            
            if (i < TableValues.Count - 1)
            {
                output += Environment.NewLine;
            }
        }

        return output;
    }
}