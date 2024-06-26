using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;

namespace PristonToolsEU;

public class BossTimeViewModel : ObservableObject, IComparable
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

    public int Favourite { get; private set; }

    public event Action<BossTimeViewModel> OnFavouriteChanged;
    public bool IsFavourite => Favourite > 0;

    public ICommand ToggleFavourite { get; }

    private void OnToggleFavourite()
    {
        if (!IsFavourite)
        {
            Favourite = 1;
        }
        else
        {
            Favourite = 0;
        }

        OnFavouriteChanged(this);
        OnPropertyChanged(nameof(IsFavourite));
        Log.Debug("Favourite for {0} is set to {1}", Boss.Name, Favourite);
    }

    public BossTimeViewModel(IBoss boss, TimeSpan timeTillBoss, IAlarm alarm)
    {
        _alarm = alarm;
        Boss = boss;
        TimeTillBoss = timeTillBoss;
        AlarmEnabled = false; // Disable alarm for all bosses by default
        ToggleFavourite = new Command(OnToggleFavourite);
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
        // 21600 is number of seconds in 6 hours, the maximum wait for any boss
        // This way favourite entries will always end up at the top of the list when sorted
        return (b.Favourite - Favourite) * 21600 + (int)(TimeTillBoss - b.TimeTillBoss).TotalSeconds;
    }
}