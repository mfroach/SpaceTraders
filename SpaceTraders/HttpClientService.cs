using System.Net.Http.Headers;
using SpaceTraders.Models;

namespace SpaceTraders;

public class HttpClientService {
    private readonly HttpClient _client;

    public HttpClientService(string token) {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<Agent?> GetAgentAsync() {
        var deserializer = new Deserializer();
        try {
            using var jsonStream = await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
            return await deserializer.DeserializeAgent(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch agent data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Deserializer.Waypoint?> GetLocationAsync() {
        // Why the hell did I do this. We should be getting location of ships. Agent doesn't move
        // todo repurpose to get any location data
        // todo implement ship location command
        var deserializer = new Deserializer();
        try {
            // Rate limit consideration: 2 reqs per second max. Try not to nest too many requests
            SpaceTraders.Models.Agent? agent = await GetAgentAsync();
            string agentWaypoint = agent.Headquarters;
            string agentSystem = agentWaypoint.Substring(0, 7); // pull system symbol out of waypoint
            await using var jsonStream =
                await _client.GetStreamAsync(
                    $"https://api.spacetraders.io/v2/systems/{agentSystem}/waypoints/{agentWaypoint}");
            return await deserializer.DeserializeWaypoint(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Deserializer.Contracts[]?> GetContractListAsync() {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await _client.GetStreamAsync("https://api.spacetraders.io/v2/my/contracts");
            return await deserializer.DeserializeContractList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch contract data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Deserializer.Contract?> GetContractAsync(string contractID) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await _client.GetStreamAsync($"https://api.spacetraders.io/v2/my/contracts/{contractID}");
            return await deserializer.DeserializeContract(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch contract data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<HttpResponseMessage> AcceptContract(string contractID) {
        return await _client.PostAsync($"https://api.spacetraders.io/v2/my/contracts/{contractID}/accept", null);
    }
}