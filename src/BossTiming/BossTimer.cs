using PristonToolsEU.BossTiming.Dto;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU.BossTiming;

public class BossTimer: IBossTimer
{
    private readonly IServerTime _serverTime;
    private readonly IBossTimePropsReader _propsReader;
    

    private BossTimeProps _valentoProps = new()
    {
        ReferenceHour = 1,
        IntervalHours = 2
    };
    
    public BossTimer(IServerTime serverTime, IBossTimePropsReader propsReader)
    {
        _serverTime = serverTime;
        _propsReader = propsReader;
        Initialise();
    }

    private async Task Initialise()
    {
        var props = await _propsReader.Read();
        // foreach (var boss in props.Bosses)
        // {
        //     Console.WriteLine(boss.Name);
        // }
    }

    private TimeSpan GetTimeUntilBoss(BossTimeProps bossTimeProps)
    {
        var nextBossHour = DateTime.Today.AddHours(bossTimeProps.ReferenceHour).AddMinutes(_serverTime.BossTimeMinute);
        
        while (nextBossHour < _serverTime.Now && TimeSpan.FromHours(bossTimeProps.IntervalHours) > TimeSpan.Zero)
        {
            nextBossHour = nextBossHour.AddHours(bossTimeProps.IntervalHours);
        }

        var result = nextBossHour - _serverTime.Now;
        return result;
    }

    public IEnumerable<string> GetBossNames()
    {
        throw new NotImplementedException();
    }

    public TimeSpan GetTimeTillBoss(string bossName)
    {
        return TimeSpan.Zero;
    }
}