using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class AgentService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Agent?> GetAgentAsync() {
        try {
            using var jsonStream = await HttpClient.GetStreamAsync("https://api.spacetraders.io/v2/my/agent");
            return await Deserializer.DeserializeAgent(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch agent data failed: {ex.Message}");
            return null;
        }
    }
}