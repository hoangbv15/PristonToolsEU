using CommunityToolkit.Mvvm.ComponentModel;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;

namespace PristonToolsEU;

public class BossTimeViewModel: ObservableObject, IComparable
{
    private readonly IAlarm _alarm;
    public IBoss Boss { get; }

    private TimeSpan _timeTillBoss;
    public TimeSpan TimeTillBoss
    {
        get => _timeTillBoss;
        set => SetProperty(ref _timeTillBoss, value);
    }

    private bool _alarmEnabled;
    private bool _isSetAlarm;

    public bool AlarmEnabled
    {
        get => _alarmEnabled;
        set
        {
            Log.Debug("Setting AlarmEnabled to {0}", value);
            _alarm.SetAlarm(Boss, value);
            SetProperty(ref _alarmEnabled, value);
        }
    }

    public bool IsSetAlarm
    {
        get => _isSetAlarm;
        set => SetProperty(ref _isSetAlarm, value);
    }

    public BossTimeViewModel(IBoss boss, TimeSpan timeTillBoss, IAlarm alarm)
    {
        _alarm = alarm;
        Boss = boss;
        TimeTillBoss = timeTillBoss;
        AlarmEnabled = true; // Enable alarm for all bosses by default
    }
    
    public static BossTimeViewModel Create(IBoss boss, TimeSpan timeTillBoss, IAlarm alarm)
    {
        return new BossTimeViewModel(boss, timeTillBoss, alarm);
    }
    
    public int CompareTo(object? o)
    {
        var b = o as BossTimeViewModel;
        if (b == null)
            return -1;
        return (int)(TimeTillBoss - b.TimeTillBoss).TotalSeconds;
    }
}