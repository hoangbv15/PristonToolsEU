using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public interface IBossTimePropsReader
{
    public Task<BossTimeProps> Read();
}