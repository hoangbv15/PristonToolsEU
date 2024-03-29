namespace PristonToolsEU.BossTiming;

public interface IBossTimer
{
    IEnumerable<string> GetBossNames();

    TimeSpan GetTimeTillBoss(string bossName);
}