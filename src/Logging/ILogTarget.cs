namespace PristonToolsEU.Logging;

public interface ILogTarget
{
    void Log(LogLevel level, string msg, object[] parameters);
}