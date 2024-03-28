namespace PristonToolsEU.Time;

public interface IServerTime
{
    DateTime Now { get; }
    
    int BossTimeMinute { get; }
}