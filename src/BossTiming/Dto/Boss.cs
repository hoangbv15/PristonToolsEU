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

public class Boss
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("referenceHour")]
    public int ReferenceHour { get; set; }
    
    [JsonPropertyName("intervalHours")]
    public int IntervalHours { get; set; }
}

public class BossArray
{
    [JsonPropertyName("bosses")]
    public List<Boss> Bosses { get; set; }
}