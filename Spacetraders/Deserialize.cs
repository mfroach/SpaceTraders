namespace Spacetraders;

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class Deserializer {
    public record Agent(
        [property: JsonPropertyName("accountId")]
        string AccountId,
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("headquarters")]
        string Headquarters,
        [property: JsonPropertyName("credits")]
        long Credits,
        [property: JsonPropertyName("startingFaction")]
        string StartingFaction,
        [property: JsonPropertyName("shipCount")]
        int ShipCount
    );

    public record WaypointData(
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("systemSymbol")]
        string SystemSymbol,
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y,
        [property: JsonPropertyName("orbitals")]
        Orbital[] Orbitals,
        [property: JsonPropertyName("traits")] 
        Trait[] Traits,
        [property: JsonPropertyName("isUnderConstruction")]
        bool IsUnderConstruction,
        [property: JsonPropertyName("faction")]
        Faction Faction,
        [property: JsonPropertyName("modifiers")]
        object[] Modifiers,
        [property: JsonPropertyName("chart")] Chart Chart
    );

    public record Orbital(
        [property: JsonPropertyName("symbol")] string Symbol
    );

    public record Trait(
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")]
        string Description
    );

    public record Faction(
        [property: JsonPropertyName("symbol")] string Symbol
    );

    public record Chart(
        [property: JsonPropertyName("waypointSymbol")]
        string WaypointSymbol,
        [property: JsonPropertyName("submittedBy")]
        string SubmittedBy,
        [property: JsonPropertyName("submittedOn")]
        DateTime SubmittedOn
    );

    private record ResponseWrapper([property: JsonPropertyName("data")] WaypointData Data);

    private record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);

    public async Task<Agent?> DeserializeAgent(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Helpful if casing isn't exact, though JsonPropertyName is preferred
        };
        try {
            var agentResponse = await JsonSerializer.DeserializeAsync<AgentResponseWrapper>(jsonStream, options);
            return agentResponse?.Data; // Return the nested Agent object
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing agent data: {ex.Message}");
            return null;
        }
    }
}