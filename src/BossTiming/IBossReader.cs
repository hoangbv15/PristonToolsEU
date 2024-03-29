using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public interface IBossReader
{
    public Task<BossArray> Read();
}