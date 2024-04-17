namespace Tsyrkov.Tgen.Interface;

public interface ILogger
{
    void Error(string message);
    void Trace(string message);

    void Error(Exception ex);
    void Trace(Exception ex);

    void Error(Exception ex, string message);
    void Trace(Exception ex, string message);
}