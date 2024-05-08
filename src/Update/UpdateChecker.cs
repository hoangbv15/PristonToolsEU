using PristonToolsEU.Networking;
using PristonToolsEU.Update.Dto;

namespace PristonToolsEU.Update;

public class UpdateChecker: IUpdateChecker
{
    private const string GetLatestReleaseUrl = "https://api.github.com/repos/hoangbv15/PristonToolsEU/releases/latest";
    
    private IRestClient _restClient;
    private Version _currentVersion;

    public UpdateChecker(IRestClient restClient)
    {
        _restClient = restClient;
        _currentVersion = new Version(CurrentVersion.Version);
    }

    public async Task<UpdateCheckResult> Check()
    {
        var latestRelease = await _restClient.Get<Release>(GetLatestReleaseUrl);
        if (string.IsNullOrWhiteSpace(latestRelease.Version))
        {
            throw new UpdateCheckException("Version check return an empty version");
        }
        
        var latestVersion = new Version(latestRelease.Version);

        if (latestVersion.CompareTo(_currentVersion) > 0)
        {
            return new UpdateCheckResult(true, latestRelease);
        }

        return new UpdateCheckResult(false, null);
    }
}