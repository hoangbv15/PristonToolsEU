namespace PristonToolsEU.Logging;

public class ConsoleLogTarget: ILogTarget
{
    public void Log(LogLevel level, string msg, object[] parameters)
    {
        var formattedMsg = string.Format(msg, parameters);
        Console.WriteLine($"[{level}] {formattedMsg}");
    }
}