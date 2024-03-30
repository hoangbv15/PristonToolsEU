using System.Text.Json;
using PristonToolsEU.BossTiming.Dto;
using PristonToolsEU.Logging;

namespace PristonToolsEU.BossTiming;

public class BossReader: IBossReader
{
    public async Task<BossArray> Read()
    {
        Log.Debug("Beginning reading boss configuration");
        await using var stream = await FileSystem.OpenAppPackageFileAsync("bosses.json");
        var deserialised = await JsonSerializer.DeserializeAsync<BossArray>(stream);
        Log.Debug("Finished reading boss configuration: \n{0}", deserialised);
        return deserialised ?? new BossArray();
    }
}