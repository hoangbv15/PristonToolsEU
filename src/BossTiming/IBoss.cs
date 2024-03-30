namespace PristonToolsEU.BossTiming;

public interface IBoss
{
    public string Name { get; set; }
    public int ReferenceHour { get; set; }
    public int IntervalHours { get; set; }
}