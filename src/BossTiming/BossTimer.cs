using PristonToolsEU.BossTiming.Dto;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU.BossTiming;

public class BossTimer: IBossTimer
{
    private readonly IServerTime _serverTime;
    private readonly IBossReader _bossReader;
    private readonly IList<Boss> _bosses = new List<Boss>();

    public BossTimer(IServerTime serverTime, IBossReader bossReader)
    {
        _serverTime = serverTime;
        _bossReader = bossReader;
    }

    public async Task Initialise()
    {
        var props = await _bossReader.Read();
        foreach (var boss in props.Bosses)
        {
            _bosses.Add(boss);
        }
    }

    public IEnumerable<IBoss> Bosses => _bosses;

    public TimeSpan GetTimeTillBoss(IBoss boss)
    {
        var nextBossHour = DateTime.Today.AddHours(boss.ReferenceHour).AddMinutes(_serverTime.BossTimeMinute);
        
        while (nextBossHour < _serverTime.Now && TimeSpan.FromHours(boss.IntervalHours) > TimeSpan.Zero)
        {
            nextBossHour = nextBossHour.AddHours(boss.IntervalHours);
        }

        var result = nextBossHour - _serverTime.Now;
        return result;
    }
}