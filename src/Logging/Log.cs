namespace PristonToolsEU.Logging;

public class Log
{
    public static readonly Log Instance = new();
    
    public LogLevel LogLevel { get; set; } = LogLevel.Debug;
    
    private readonly IList<ILogTarget> _logTargets = new List<ILogTarget>();

    public void AddLogTarget(ILogTarget target)
    {
        _logTargets.Add(target);
    }
    
    private void InternalLog(LogLevel level, string msg, object[] parameters)
    {
        if (LogLevel > level)
        {
            return;
        }

        foreach (var logTarget in _logTargets)
        {
            logTarget.Log(level, msg, parameters);    
        }
    }

    public static void Debug(string msg, params object[] parameters)
    {
        Instance.InternalLog(LogLevel.Debug, msg, parameters);
    }
    
    public static void Info(string msg, params object[] parameters)
    {
        Instance.InternalLog(LogLevel.Info, msg, parameters); 
    }

    public static void Warn(string msg, params object[] parameters)
    {
        Instance.InternalLog(LogLevel.Warn, msg, parameters); 
    }

    public static void Error(string msg, params object[] parameters)
    {
        Instance.InternalLog(LogLevel.Error, msg, parameters); 
    }

}