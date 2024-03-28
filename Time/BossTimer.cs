namespace PristonToolsEU.Time;

public class BossTimer: IBossTimer
{
    private readonly IServerTime _serverTime;

    public TimeSpan Valento => GetTimeUntilBoss(_valentoProps);
    public TimeSpan Kelvezu { get; }
    public TimeSpan Mokova { get; }
    public TimeSpan Devil { get; }
    public TimeSpan Tulla { get; }
    public TimeSpan Draxos { get; }
    public TimeSpan Greedy { get; }
    public TimeSpan Yagditha { get; }
    public TimeSpan Ignis { get; }
    public TimeSpan Primal { get; }
    public TimeSpan Aragonia { get; }

    private BossTimeProps _valentoProps = new()
    {
        ReferenceTime = DateTime.Today.AddHours(1),
        Interval = TimeSpan.FromHours(2)
    };
    
    public BossTimer(IServerTime serverTime)
    {
        _serverTime = serverTime;
    }

    private TimeSpan GetTimeUntilBoss(BossTimeProps bossTimeProps)
    {
        var nextBossHour = bossTimeProps.ReferenceTime.AddMinutes(_serverTime.BossTimeMinute);
        
        while (nextBossHour < _serverTime.Now && bossTimeProps.Interval > TimeSpan.Zero)
        {
            nextBossHour = nextBossHour.Add(bossTimeProps.Interval);
        }

        var result = nextBossHour - _serverTime.Now;
        return result;
    }
}