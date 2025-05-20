namespace Spacetraders;

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class Deserializer {
    // Can we define these records in another file for readability?
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

    public record Contracts( // rename ContractsList?
        [property: JsonPropertyName("id")]
        string ContractID,
        [property: JsonPropertyName("factionSymbol")]
        string FactionSymbol,
        [property: JsonPropertyName("type")]
        string ContractType,
        [property: JsonPropertyName("terms")]
        Terms Terms,
        [property: JsonPropertyName("accepted")]
        bool Accepted
    );

    public record Contract( // todo define the rest of the properties
        [property: JsonPropertyName("id")]
        string ContractID,
        [property: JsonPropertyName("factionSymbol")]
        string FactionSymbol,
        [property: JsonPropertyName("type")]
        string ContractType,
        [property: JsonPropertyName("terms")]
        Terms Terms,
        [property: JsonPropertyName("accepted")]
        bool Accepted
    );
    
    public record Terms(
        [property: JsonPropertyName("deadline")] DateTime Deadline,
        [property: JsonPropertyName("payment")] Payment Payment,
        [property: JsonPropertyName("deliver")] Deliver[] DeliverItems
    );

    public record Payment(
        [property: JsonPropertyName("onAccepted")] int OnAccepted,
        [property: JsonPropertyName("onFulfilled")] int OnFulfilled
    );

    public record Deliver(
        [property: JsonPropertyName("tradeSymbol")] string TradeSymbol,
        [property: JsonPropertyName("destinationSymbol")] string DestinationSymbol,
        [property: JsonPropertyName("unitsRequired")] int UnitsRequired,
        [property: JsonPropertyName("unitsFulfilled")] int UnitsFulfilled
    );

    // Can we aggregate the response wrapper into one record?
    private record WaypointResponseWrapper([property: JsonPropertyName("data")] Waypoint Data);
    private record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);
    private record ContractListResponseWrapper([property: JsonPropertyName("data")] Contracts[] Data);
    private record ContractResponseWrapper([property: JsonPropertyName("data")] Contract Data);

    
    public async Task<Agent?> DeserializeAgent(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<AgentResponseWrapper>(jsonStream, options);
            return response?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing agent data: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Waypoint?> DeserializeWaypoint(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<WaypointResponseWrapper>(jsonStream, options);
            return response?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing waypoint data: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Contracts[]?> DeserializeContracts(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<ContractListResponseWrapper>(jsonStream, options);
            return response?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing contracts data: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Contract?> DeserializeContract(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<ContractResponseWrapper>(jsonStream, options);
            return response?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing contract data: {ex.Message}");
            return null;
        }
    }
}