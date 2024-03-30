using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public interface IBossTimer
{
    Task Initialise();
    
    IEnumerable<IBoss> Bosses { get; }

    TimeSpan GetTimeTillBoss(IBoss boss);
}