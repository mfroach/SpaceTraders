using System.Net.Http.Headers;
using Spacetraders;

public class HttpClientService {
    private readonly HttpClient _client;

    public HttpClientService(string token) {
        token = $"Bearer {token}";
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", token);
    }

    public async Task<string> GetAgentAsync() {
        var DeserializeJSON = new Deserializer();
        var json = await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
        json = DeserializeJSON.DeserializeAgent(json);
        return json;
    }
    public async Task<string> GetLocationAsync() {
        var json = await _client.GetStringAsync("https://api.spacetraders.io/v2/systems/X1-J90/waypoints/X1-J90-A1");
        return json;
    }
}
