using PristonToolsEU.BossTiming.Dto;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU.BossTiming;

public class BossTimer: IBossTimer
{
    
    private readonly IServerTime _serverTime;
    private readonly IBossReader _propsReader;
    private readonly IDictionary<string, Boss> _bossProps = new Dictionary<string, Boss>();
    private readonly IList<Boss> _bosses = new List<Boss>();

    public BossTimer(IServerTime serverTime, IBossReader propsReader)
    {
        _serverTime = serverTime;
        _propsReader = propsReader;
    }

    public async Task Initialise()
    {
        var props = await _propsReader.Read();
        foreach (var boss in props.Bosses)
        {
            _bossProps[boss.Name] = boss;
            _bosses.Add(boss);
        }
    }

    private TimeSpan GetTimeUntilBoss(Boss boss)
    {
        var nextBossHour = DateTime.Today.AddHours(boss.ReferenceHour).AddMinutes(_serverTime.BossTimeMinute);
        
        while (nextBossHour < _serverTime.Now && TimeSpan.FromHours(boss.IntervalHours) > TimeSpan.Zero)
        {
            nextBossHour = nextBossHour.AddHours(boss.IntervalHours);
        }

        var result = nextBossHour - _serverTime.Now;
        return result;
    }

    public IEnumerable<Boss> Bosses => _bosses;

    public TimeSpan GetTimeTillBoss(string bossName)
    {
        return GetTimeUntilBoss(_bossProps[bossName]);
    }
}