namespace Tsyrkov.Tgen;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2 || args.Length == 0) { PrintUsage(); return; }

        Logger log = new Logger();

        Core.Core core = new Core.Core(log);

        core.LoadTemplateFromFile(args[0]);
        core.LoadTableValuesFromCSV(args[1]);

        string result = core.RenderTemplate();
        Console.WriteLine("RESULT:");
        Console.Write(result);
    }

    public static void PrintUsage()
    {
        Console.WriteLine("USAGE:");
        Console.WriteLine("tgen [template file path] [values file path]");
    }
}
