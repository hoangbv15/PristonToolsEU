using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public interface IBossTimer
{
    Task Initialise();
    
    IEnumerable<Boss> Bosses { get; }

    TimeSpan GetTimeTillBoss(string bossName);
}