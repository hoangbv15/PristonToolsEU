using System.Text;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;

namespace PristonToolsEU.Alarming;

public class Alarm : IAlarm
{
    private readonly IBossTimer _bossTimer;
    private readonly int[] _milestones = { 1, 2, 3, 5, 10, 15, 30, 60 }; // in minutes

    private readonly Timer _timer;
    private HashSet<IBoss> _alarms = new();

    public Alarm(IBossTimer bossTimer)
    {
        _bossTimer = bossTimer;

        // Prepare the alarms so we don't recalculate this every second
        // Convert minutes to seconds
        for (int i = 0; i < _milestones.Length; i++)
        {
            _milestones[i] *= 60;
        }

        _timer = new Timer(o => Update(o), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    ~Alarm()
    {
        _timer.Dispose();
    }

    private async Task Update(object? state)
    {
        var toBeAnnounced = new List<Tuple<IBoss, int>>();

        foreach (var boss in _alarms)
        {
            foreach (var milestone in _milestones)
            {
                var timeTillBoss = _bossTimer.GetTimeTillBoss(boss);
                if ((int)timeTillBoss.TotalSeconds == milestone)
                {
                    toBeAnnounced.Add(Tuple.Create(boss, milestone / 60));
                }
            }
        }

        await Announce(toBeAnnounced);
    }

    private async Task Announce(IList<Tuple<IBoss, int>> toBeAnnounced)
    {
        if (!toBeAnnounced.Any())
        {
            return;
        }

        var announceCategories = new Dictionary<int, IList<IBoss>>();
        foreach (var tuple in toBeAnnounced)
        {
            var boss = tuple.Item1;
            var minute = tuple.Item2;
            if (!announceCategories.ContainsKey(minute))
            {
                announceCategories[minute] = new List<IBoss>();
            }

            announceCategories[minute].Add(boss);
        }

        foreach (var minute in announceCategories.Keys)
        {
            var sb = new StringBuilder();
            sb.Append(minute);
            sb.Append(" minutes until ");
            for (var i = 0; i < announceCategories[minute].Count; i++)
            {
                var boss = announceCategories[minute][i];
                sb.Append(GetTextToAnnounce(boss));
                if (i == announceCategories[minute].Count - 2)
                {
                    sb.Append(", and ");
                }
                else if (i < announceCategories[minute].Count - 2)
                {
                    sb.Append(", ");
                }
            }

            var announceSentence = sb.ToString();
            Log.Info(announceSentence);
            await TextToSpeech.Default.SpeakAsync(announceSentence);
        }
    }

    private string GetTextToAnnounce(IBoss boss)
    {
        if (boss.TextToSpeech != null)
        {
            return boss.TextToSpeech;
        }

        return boss.Name;
    }

    public void SetAlarm(IBoss boss, bool isSet)
    {
        if (isSet)
        {
            _alarms.Add(boss);
        }
        else
        {
            _alarms.Remove(boss);
        }

        Log.Info("Alarm for {0} is set to {1}", boss.Name, isSet);
    }
}