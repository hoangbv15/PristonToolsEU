using CommunityToolkit.Mvvm.ComponentModel;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;

namespace PristonToolsEU;

public class BossTimeViewModel: ObservableObject
{
    private readonly IAlarm _alarm;
    public IBoss Boss { get; }

    private TimeSpan _timeTillBoss;
    public TimeSpan TimeTillBoss
    {
        get => _timeTillBoss;
        set => SetProperty(ref _timeTillBoss, value);
    }

    private bool _alarmEnabled = false;
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

    public BossTimeViewModel(IBoss boss, TimeSpan timeTillBoss, IAlarm alarm)
    {
        _alarm = alarm;
        Boss = boss;
        TimeTillBoss = timeTillBoss;
    }
    
    public static BossTimeViewModel Create(IBoss boss, TimeSpan timeTillBoss, IAlarm alarm)
    {
        return new BossTimeViewModel(boss, timeTillBoss, alarm);
    }
}