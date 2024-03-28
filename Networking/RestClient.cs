using System.Net.Http.Headers;
using System.Text.Json;

namespace PristonToolsEU.Networking;

public class RestClient: IRestClient
{
    private HttpClient _httpClient;
    
    public RestClient()
    {
        _httpClient = new HttpClient();
        // _httpClient.DefaultRequestHeaders.Accept.Clear();
        // _httpClient.DefaultRequestHeaders.Accept.Add(
        //     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        // _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
    }
    
    public async Task<T> Get<T>(string url) where T : new()
    {
        await using Stream stream =
            await _httpClient.GetStreamAsync(url);
        var deserialised =
            await JsonSerializer.DeserializeAsync<T>(stream);
        return deserialised ?? new T();
    }
}