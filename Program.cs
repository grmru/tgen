using System;

namespace Tsyrkov.Tgen;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2 || args.Length == 0) { PrintUsage(); }

        

    }

    public static void PrintUsage()
    {
        Console.WriteLine("USAGE:");
        Console.WriteLine("tgen [template file path] [values in JSON file path]");
    }
}
