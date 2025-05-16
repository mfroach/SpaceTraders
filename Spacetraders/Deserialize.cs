namespace Spacetraders;

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization; // Required for JsonPropertyName
using System.Threading.Tasks;

public class Deserializer {
    // Define the Agent record based on the expected fields from the API
    // Ensure this record is public if HttpClientService or Program.cs needs to access its type directly.
    public record Agent(
        [property: JsonPropertyName("accountId")] string AccountId,
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("headquarters")] string Headquarters,
        [property: JsonPropertyName("credits")] long Credits,
        [property: JsonPropertyName("startingFaction")] string StartingFaction,
        [property: JsonPropertyName("shipCount")] int ShipCount
    );

    // Helper record to match the API's typical {"data": ...} structure
    private record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);

    // Updated method to deserialize the JSON stream into an Agent object
    public async Task<Agent?> DeserializeAgent(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Helpful if casing isn't exact, though JsonPropertyName is preferred
        };
        try {
            var agentResponse = await JsonSerializer.DeserializeAsync<AgentResponseWrapper>(jsonStream, options);
            return agentResponse?.Data; // Return the nested Agent object
        }
        catch (JsonException ex) {
            // Log the exception or handle it as appropriate for your application
            Console.WriteLine($"Error deserializing agent data: {ex.Message}");
            return null;
        }
    }
}