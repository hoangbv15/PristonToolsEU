namespace PristonToolsEU.Update;

public class UpdateCheckResult
{
    public UpdateCheckResult(bool hasNewUpdate, IRelease? updateInfo)
    {
        HasNewUpdate = hasNewUpdate;
        UpdateInfo = updateInfo;
    }

    public bool HasNewUpdate { get; }
    public IRelease? UpdateInfo { get; }
}