using System.Text.Json;
public class Deserializer {
    public record class Agent (string symbol);
    public async Task<List<Agent>> DeserializeAgent(System.IO.Stream json) {
        var agent = 
            await JsonSerializer.DeserializeAsync<List<Agent>>(json);
        return agent;
    }
}