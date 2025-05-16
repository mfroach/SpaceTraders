using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json; // For JsonException if you handle it here
using System.Threading.Tasks;
using Spacetraders; // To access Deserializer and Deserializer.Agent

public class HttpClientService {
    private readonly HttpClient _client;

    public HttpClientService(string token) {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        // Specify that JSON responses are expected
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // Set the authorization header correctly for a Bearer token
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    // GetAgentAsync now returns Task<Deserializer.Agent?>
    public async Task<Deserializer.Agent?> GetAgentAsync() {
        var deserializer = new Deserializer();
        try {
            // Get the response as a stream
            using var jsonStream = await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
            // Deserialize the stream to an Agent object
            return await deserializer.DeserializeAgent(jsonStream);
        }
        catch (HttpRequestException ex) {
            // Handle HTTP request errors (e.g., network issues, API returning an error status)
            Console.WriteLine($"HTTP request to fetch agent data failed: {ex.Message}");
            return null;
        }
        // JsonException is handled within DeserializeAgent in this example,
        // but could also be caught here if preferred.
    }

    public async Task<string> GetLocationAsync() {
        try {
            var json = await _client.GetStringAsync("https://api.spacetraders.io/v2/systems/X1-J90/waypoints/X1-J90-A1");
            return json;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return $"Error fetching location: {ex.Message}"; // Return an error message string
        }
    }
}