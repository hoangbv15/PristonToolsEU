namespace PristonToolsEU.Update;

public class UpdateCheckException: Exception
{
    public UpdateCheckException(string message) : base(message)
    {
    }
}