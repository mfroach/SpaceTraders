using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json; // For JsonException if you handle it here
using System.Threading.Tasks;
using Spacetraders;

public class HttpClientService {
    private readonly HttpClient _client;

    public HttpClientService(string token) {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<Deserializer.Agent?> GetAgentAsync() {
        var deserializer = new Deserializer();
        try {
            // Get the response as a stream
            using var jsonStream = await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
            // Deserialize the stream to an Agent object
            return await deserializer.DeserializeAgent(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch agent data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Deserializer.Waypoint> GetLocationAsync() {
        var deserializer = new Deserializer();
        try {
            Deserializer.Agent? agent = await GetAgentAsync();
            // how do we get token here?
            string agentWaypoint = agent.Headquarters;
            string agentSystem = agentWaypoint.Substring(0, 7);
            await using var jsonStream = await _client.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{agentSystem}/waypoints/{agentWaypoint}");
            return await deserializer.DeserializeWaypoint(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }
}