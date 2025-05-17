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

    public async Task<string> GetLocationAsync() {
        try {
            // this is wrong
            var json = await _client.GetStringAsync("https://api.spacetraders.io/v2/systems/X1-ZC45/waypoints/X1-ZC45-A1");
            return json;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return $"Error fetching location: {ex.Message}"; // Return an error message string
        }
    }
}