using System.Text.Json.Serialization;

namespace PristonToolsEU.ServerTiming.Dto;

public class ServerDetail
{
    [JsonPropertyName("boss.time.second")] public int BossTimeSecond { get; set; }

    [JsonPropertyName("server.game.unix.time")]
    public int ServerGameUnixTime { get; set; }

    [JsonPropertyName("server.login.unix.time")]
    public int ServerLoginUnixTime { get; set; }

    [JsonPropertyName("under.maintenance")]
    public int UnderMaintenance { get; set; }

    [JsonPropertyName("users.online")] public int UsersOnline { get; set; }
}