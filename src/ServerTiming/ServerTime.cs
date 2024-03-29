using System.Timers;
using PristonToolsEU.Networking;
using PristonToolsEU.ServerTiming.Dto;
using Timer = System.Timers.Timer;

namespace PristonToolsEU.ServerTiming;

public class ServerTime : IServerTime, IDisposable
{
    private const int SyncIntervalMs = 1800000; // 30 minutes
    private const string PteuTimeUrl = $"https://pristontale.eu/api/api.php?key={ApiKey}";
    private const string ApiKey = "c4b90e23c554d10c3c9deadcdbfcf93b";
    
    private readonly IRestClient _restClient;
    private TimeSpan _serverTimeOffset = TimeSpan.Zero;
    private int _bossTimeMinute;
    
    private readonly Timer _timer;

    public DateTime Now => DateTime.UtcNow + _serverTimeOffset;
    public int BossTimeMinute => _bossTimeMinute;

    public ServerTime(IRestClient restClient)
    {
        _restClient = restClient;
        _timer = new Timer(SyncIntervalMs);
        _timer.Elapsed += OnSyncInterval;
        _timer.AutoReset = true;
        _timer.Start();
        Sync();
    }

    private void OnSyncInterval(object? sender, ElapsedEventArgs e)
    {
        Sync();
    }

    private async void Sync()
    {
        var serverTime = await _restClient.Get<PteuTime>(PteuTimeUrl);
        if (serverTime.Babel == null)
        {
            throw new Exception("Server returned null Babel field");
        }
        _serverTimeOffset = DateTime.UtcNow - DateTimeOffset.FromUnixTimeSeconds(serverTime.Babel.ServerGameUnixTime).DateTime;
        _bossTimeMinute = serverTime.Babel.BossTimeSecond; 
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}