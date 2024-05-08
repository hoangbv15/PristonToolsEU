using System.Text.Json.Serialization;

namespace PristonToolsEU.Update.Dto;

public class Release: IRelease
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("name")]
    public string Version { get; set; }
}