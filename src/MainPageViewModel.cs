using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IBossTimer _bossTimer;
    private readonly IServerTime _serverTime;
    private readonly IAlarm _alarm;
    private readonly PeriodicTimer _timer;
    private CancellationTokenSource _sortCancellationToken = new();
    private Task _sortTask;

    public ObservableCollection<BossTimeViewModel> Bosses { get; private set; } = new();
    
    public ICommand RefreshBosses { get; } 
    public ICommand SetAlarm { get; }
    public ICommand SortByTime { get; }
    public bool IsRefreshingBosses { get; set; }
    private async Task ExecuteRefreshBosses()
    {
        await _serverTime.Sync();
        IsRefreshingBosses = false;
    }
    
    public MainPageViewModel(IBossTimer bossTimer, IServerTime serverTime, IAlarm alarm)
    {
        _bossTimer = bossTimer;
        _serverTime = serverTime;
        _alarm = alarm;

        RefreshBosses = new Command(async () => await ExecuteRefreshBosses());
        SortByTime = new Command(OnSortByTime);
        SetAlarm = new Command(OnSetAlarm);
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        
        InitialiseBossTimer();
    }

    private void OnSetAlarm()
    {
        Log.Debug("OnSetAlarm clicked");

    }

    private async void InitialiseBossTimer()
    {
        await _bossTimer.Initialise();
        var bossTimes = new List<BossTimeViewModel>();
        foreach (var boss in _bossTimer.Bosses)
        {
            var bossTimeViewModel = BossTimeViewModel.Create(boss, _bossTimer.GetTimeTillBoss(boss), _alarm);
            bossTimes.Add(bossTimeViewModel);
            bossTimeViewModel.OnFavouriteChanged += OnFavouriteChanged;
        }
        Bosses = new ObservableCollection<BossTimeViewModel>(bossTimes);
        OnPropertyChanged(nameof(Bosses));
        StartUpdating();
    }
    
    private void OnSortByTime()
    {
        Log.Debug("OnSortByTime clicked");
        RunSort(Bosses.Sort);
    }

    private void OnFavouriteChanged()
    {
        RunSort(Bosses.SortByFavourite);
    }

    private void RunSort(Action<CancellationToken> sortAction)
    {
        _sortCancellationToken.Cancel();
        _sortTask?.Wait();
        _sortCancellationToken = new CancellationTokenSource();
        var token = _sortCancellationToken.Token;
        _sortTask = Task.Run(() => sortAction(token), token);
    }

    ~MainPageViewModel() 
    {
        _timer.Dispose();
    }

    private async Task StartUpdating()
    {
        while (await _timer.WaitForNextTickAsync())
        {
            foreach (var boss in Bosses)
            {
                boss.TimeTillBoss = _bossTimer.GetTimeTillBoss(boss.Boss);
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}