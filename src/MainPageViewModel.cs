using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PristonToolsEU.BossTiming;
using PristonToolsEU.BossTiming.Dto;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IBossTimer _bossTimer;
    private readonly Timer _timer;

    public ObservableCollection<Boss> Bosses { get; private set; } = new();

    public Dictionary<string, TimeSpan> BossTimes { get; } = new();

    public MainPageViewModel(IBossTimer bossTimer)
    {
        _bossTimer = bossTimer;
        _timer = new Timer(Update,
                           null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        InitialiseBossTimer();
    }

    private async void InitialiseBossTimer()
    {
        await _bossTimer.Initialise();
        Bosses = new ObservableCollection<Boss>(_bossTimer.Bosses);
        OnPropertyChanged(nameof(Bosses));
        Update(null);
    }

    ~MainPageViewModel() 
    {
        _timer.Dispose();
    }

    private void Update(object? state)
    {
        foreach (var boss in Bosses)
        {
            BossTimes[boss.Name] = _bossTimer.GetTimeTillBoss(boss.Name);
        }

        OnPropertyChanged(nameof(Bosses));
        OnPropertyChanged(nameof(BossTimes));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}