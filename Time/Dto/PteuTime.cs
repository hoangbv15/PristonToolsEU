using System.Text.Json.Serialization;

namespace PristonToolsEU.Time.Dto;

public class PteuTime
{
    [JsonPropertyName("online_babel")]
    public bool IsBabelOnline { get; set; }
    
    [JsonPropertyName("online_seasonal")]
    public bool IsSeasonalOnline { get; set; }
    
    [JsonPropertyName("babel")]
    public ServerDetail? Babel { get; set; }
    
    [JsonPropertyName("seasonal")]
    public ServerDetail? Seasonal { get; set; }
}
