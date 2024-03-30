using PristonToolsEU.BossTiming;

namespace PristonToolsEU.Alarming;

public interface IAlarm
{ 
    void SetAlarm(IBoss boss, bool isSet);
}