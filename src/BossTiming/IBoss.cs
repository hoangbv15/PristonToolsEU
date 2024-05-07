namespace PristonToolsEU.BossTiming;

public interface IBoss
{
    public string Name { get; set; }
    public string? TextToSpeech { get; set; }
    public int FirstHour { get; set; }
    public int IntervalHours { get; set; }
    public int? MinuteOverride { get; set; }
}