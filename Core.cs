using Tsyrkov.Tgen.Interface;

namespace Tsyrkov.Tgen.Core;

public class Core
{
    private ILogger _log { get; }
    public string DataTemplate { get; set; } = string.Empty;
    public List<string[]> TableValues { get; set; } = [];

    public Core(ILogger logger)
    {
        this._log = logger;
    }

    public void LoadTemplateFromFile(string filePath)
    {
        try
        {
            DataTemplate = System.IO.File.ReadAllText(filePath);
        }
        catch (Exception ex) { _log.Error(ex); }
    }

    public void LoadTableValuesFromCSV(string filePath, string separator = ";")
    {
        string[] lines = [];
        try
        {
            lines = System.IO.File.ReadAllLines(filePath);
        }
        catch (Exception ex) { _log.Error(ex); }

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] values = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TableValues.Add(values);
        }
    }

    public string RenderTemplate()
    {
        if (TableValues.Count == 0) return string.Empty;

        string ret = string.Empty;

        for (int i = 0; i < TableValues.Count; i++) 
        { 
            string item = new string(DataTemplate);
            for (int j = 0; j < TableValues[i].Length; j++)
            {
                item = item.Replace($"@@{j}@@", TableValues[i][j].Trim());
            }
            ret += item;
            // ret += System.Environment.NewLine;
            // ret += "// -----";
            ret += System.Environment.NewLine;
        }

        return ret;
    }
}