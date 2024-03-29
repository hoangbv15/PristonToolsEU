using System.Text.Json;
using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public class BossTimePropsReader: IBossTimePropsReader
{
    public async Task<BossTimeProps> Read()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("bossTimeProps.json");
        var deserialised = await JsonSerializer.DeserializeAsync<BossTimeProps>(stream);
        return deserialised ?? new BossTimeProps();
    }
}