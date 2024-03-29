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

public class BossTimeProps
{
    [JsonPropertyName("name")] 
    public string Name;
    
    [JsonPropertyName("referenceHour")]
    public int ReferenceHour;
    
    [JsonPropertyName("intervalHours")]
    public int IntervalHours;
}

public class BossTimePropsArray
{
    [JsonPropertyName("bosses")]
    public List<BossTimeProps> Bosses;
}