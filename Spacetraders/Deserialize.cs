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

    public record Waypoint(
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

    // Can we aggregate the response wrapper into one record? Will need one per query doing like this.
    private record WaypointResponseWrapper([property: JsonPropertyName("data")] Waypoint Data);

    private record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);

    public async Task<Agent?> DeserializeAgent(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var agentResponse = await JsonSerializer.DeserializeAsync<AgentResponseWrapper>(jsonStream, options);
            return agentResponse?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing data: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Waypoint?> DeserializeWaypoint(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var waypointResponse = await JsonSerializer.DeserializeAsync<WaypointResponseWrapper>(jsonStream, options);
            return waypointResponse?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing data: {ex.Message}");
            return null;
        }
    }
}