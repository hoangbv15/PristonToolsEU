using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;
using PristonToolsEU.ServerTiming;
using PristonToolsEU.Update;

namespace PristonToolsEU;

public class MainPageViewModel : INotifyPropertyChanged
{
    private int NumOfFavourites = 0;
    private readonly IBossTimer _bossTimer;
    private readonly IServerTime _serverTime;
    private readonly IAlarm _alarm;
    private readonly PeriodicTimer _timer;
    private CancellationTokenSource _sortCancellationToken = new();
    private Task _sortTask;
    private bool _isAllAlarmOn;

    public FastObservableCollection<BossTimeViewModel> Bosses { get; private set; } = new();
    
    public ICommand RefreshBosses { get; } 
    public ICommand ToggleAllAlarm { get; }
    public ICommand SortByTime { get; }
    public bool IsRefreshingBosses { get; set; }

    public bool IsAllAlarmOn
    {
        get => _isAllAlarmOn;
        set => _isAllAlarmOn = value;
    }

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
        ToggleAllAlarm = new Command(OnToggleAllAlarm);
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        
        InitialiseBossTimer();
    }

    private void OnToggleAllAlarm()
    {
        Log.Debug("OnToggleAllAlarm clicked");
        IsAllAlarmOn = !IsAllAlarmOn;
        OnPropertyChanged(nameof(IsAllAlarmOn));
        foreach (var boss in Bosses)
        {
            boss.AlarmEnabled = IsAllAlarmOn;
        }
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
        Bosses = new FastObservableCollection<BossTimeViewModel>(bossTimes);
        OnPropertyChanged(nameof(Bosses));
        StartUpdating();
    }

    private void OnFavouriteChanged(BossTimeViewModel viewModel)
    {
        if (viewModel.IsFavourite)
        {
            NumOfFavourites++;
            Bosses.Move(Bosses.IndexOf(viewModel), 0);
        }
        else
        {
            NumOfFavourites--;
            Bosses.Move(Bosses.IndexOf(viewModel), NumOfFavourites);
        }
    }
    
    private void OnSortByTime()
    {
        Log.Debug("OnSortByTime clicked");
        RunSort(Bosses.Sort);
    }

    private void RunSort(Action<CancellationToken, SynchronizationContext> sortAction)
    {
        _sortCancellationToken.Cancel();
        try
        {
            _sortTask?.Wait();
        }
        catch (AggregateException _) 
        { 
            // Do nothing, exception is expected here
        }
        _sortCancellationToken = new CancellationTokenSource();
        var token = _sortCancellationToken.Token;
        var sctx = SynchronizationContext.Current;

        _sortTask = Task.Run(() => sortAction(token, sctx), token);
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