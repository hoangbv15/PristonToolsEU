namespace PristonToolsEU.Update;

public interface IUpdateChecker
{
    Task<UpdateCheckResult> Check();
}