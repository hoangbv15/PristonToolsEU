using System.ComponentModel;
using System.Runtime.CompilerServices;
using PristonToolsEU.BossTiming;
using PristonToolsEU.ServerTiming;

namespace PristonToolsEU;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IBossTimer _bossTimer;
    private readonly Timer _timer;

    private TimeSpan _valento;

    public TimeSpan Valento
    {
        get => _valento;
        set
        {
            _valento = value;
            OnPropertyChanged();
        }
    }

    public MainPageViewModel(IBossTimer bossTimer)
    {
        _bossTimer = bossTimer;
        _timer = new Timer(new TimerCallback(UpdateLabels),
                           null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }
    
    ~MainPageViewModel() 
    {
        _timer.Dispose();
    }

    private void UpdateLabels(object? state)
    {
        Valento = _bossTimer.GetTimeTillBoss("Valento");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}