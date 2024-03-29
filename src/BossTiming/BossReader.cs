using System.Text.Json;
using PristonToolsEU.BossTiming.Dto;

namespace PristonToolsEU.BossTiming;

public class BossReader: IBossReader
{
    public async Task<BossArray> Read()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("bosses.json");
        var deserialised = await JsonSerializer.DeserializeAsync<BossArray>(stream);
        return deserialised ?? new BossArray();
    }
}