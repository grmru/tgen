using Tsyrkov.Tgen.Interface;

namespace Tsyrkov.Tgen;

public class Logger : ILogger
{
    public void Error(string message)
    {
        Write("ERROR", null, message);
    }

    public void Error(Exception ex)
    {
        Write("ERROR", ex, string.Empty);
    }

    public void Error(Exception ex, string message)
    {
        Write("ERROR", ex, message);
    }

    public void Trace(string message)
    {
        Write("TRACE", null, message);
    }

    public void Trace(Exception ex)
    {
        Write("TRACE", ex, string.Empty);
    }

    public void Trace(Exception ex, string message)
    {
        Write("TRACE", ex, message);
    }

    private void Write(string level, Exception? ex, string message)
    {
        Console.WriteLine($"{DateTime.Now} [{level}]: {message}");
        if (ex != null)
        {
            Console.WriteLine("--- Exception:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }
}