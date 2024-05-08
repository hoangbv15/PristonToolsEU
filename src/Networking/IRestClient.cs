namespace PristonToolsEU.Networking;

public interface IRestClient
{
    Task<T> Get<T>(string url) where T : new();
}