using System.Text.Json.Serialization;

namespace PristonToolsEU.BossTiming.Dto;

// {
//     "bosses": [
//     {
//         "name": "Valento",
//         "referenceHour": 1,
//         "intervalHours": 2
//     }
//     ]
// }

public class Boss: IBoss
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }

    [JsonPropertyName("textToSpeech")]
    public string? TextToSpeech { get; set; }
    
    [JsonPropertyName("firstHour")]
    public int FirstHour { get; set; }
    
    [JsonPropertyName("intervalHours")]
    public int IntervalHours { get; set; }

    [JsonPropertyName("minuteOverride")]
    public int? MinuteOverride { get; set; }
}

public class BossArray
{
    [JsonPropertyName("bosses")]
    public List<Boss> Bosses { get; set; }
}