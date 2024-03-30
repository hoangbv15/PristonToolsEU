using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;

namespace PristonToolsEU.Alarming;

public class Alarm: IAlarm
{
    private readonly IBossTimer _bossTimer;
    private readonly int[] _milestones = { 1, 2, 3, 5, 10, 15, 30, 60 }; // in minutes

    private readonly Timer _timer;
    private IDictionary<IBoss, bool> _alarms = new Dictionary<IBoss, bool>();

    public Alarm(IBossTimer bossTimer)
    {
        _bossTimer = bossTimer;
        _timer = new Timer(o => Update(o), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        
        // Prepare the alarms so we don't recalculate this every second
        // Convert minutes to seconds
        for (int i = 0; i < _milestones.Length; i++)
        {
            _milestones[i] *= 60;
        }
    }

    ~Alarm()
    {
        _timer.Dispose();
    }

    private async Task Update(object? state)
    {
        var toBeAnnounced = new List<Tuple<IBoss, int>>();
        
        foreach (var alarm in _alarms)
        {
            var boss = alarm.Key;
            var isSet = alarm.Value;

            if (!isSet)
            {
                continue;
            }

            
            foreach (var milestone in _milestones)
            {
                var timeTillBoss = _bossTimer.GetTimeTillBoss(boss);
                if ((int)timeTillBoss.TotalSeconds == milestone)
                {
                    toBeAnnounced.Add(Tuple.Create(boss, milestone));
                }
            }
        }
        
        foreach (var item in toBeAnnounced)
        {
            await Announce(item.Item1, item.Item2);
        }
    }

    private async Task Announce(IBoss boss, int minutes)
    {
        Log.Info("{0} minutes until {1}!", minutes, boss.Name);
        await TextToSpeech.Default.SpeakAsync($"{minutes} minutes until {boss.Name}!");
    }

    public void SetAlarm(IBoss boss, bool isSet)
    {
        _alarms[boss] = isSet;
        Log.Info("Alarm for {0} is set to {1}", boss.Name, isSet);
    }
}