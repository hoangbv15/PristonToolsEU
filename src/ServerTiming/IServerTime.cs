namespace PristonToolsEU.ServerTiming;

public interface IServerTime
{
    DateTime Now { get; }

    int BossTimeMinute { get; }

    Task Sync();
}