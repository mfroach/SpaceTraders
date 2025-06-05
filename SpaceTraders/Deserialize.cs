namespace SpaceTraders;

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SpaceTraders.Models; // To access Account, AccountApiResponse

public class Deserializer {
// todo refactor orbital and faction out of deserialize
    public record Waypoint(
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("systemSymbol")]
        string SystemSymbol,
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y,
        [property: JsonPropertyName("orbitals")]
        Orbital[] Orbitals,
        [property: JsonPropertyName("traits")] Trait[] Traits,
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
        [property: JsonPropertyName("id")] string ContractID,
        [property: JsonPropertyName("factionSymbol")]
        string FactionSymbol,
        [property: JsonPropertyName("type")] string ContractType,
        [property: JsonPropertyName("terms")] Terms Terms,
        [property: JsonPropertyName("accepted")]
        bool Accepted
    );

    public record Contract( // todo define the rest of the properties
        [property: JsonPropertyName("id")] string ContractID,
        [property: JsonPropertyName("factionSymbol")]
        string FactionSymbol,
        [property: JsonPropertyName("type")] string ContractType,
        [property: JsonPropertyName("terms")] Terms Terms,
        [property: JsonPropertyName("accepted")]
        bool Accepted
    );

    public record Terms(
        [property: JsonPropertyName("deadline")]
        DateTime Deadline,
        [property: JsonPropertyName("payment")]
        Payment Payment,
        [property: JsonPropertyName("deliver")]
        Deliver[] DeliverItems
    );

    public record Payment(
        [property: JsonPropertyName("onAccepted")]
        int OnAccepted,
        [property: JsonPropertyName("onFulfilled")]
        int OnFulfilled
    );

    public record Deliver(
        [property: JsonPropertyName("tradeSymbol")]
        string TradeSymbol,
        [property: JsonPropertyName("destinationSymbol")]
        string DestinationSymbol,
        [property: JsonPropertyName("unitsRequired")]
        int UnitsRequired,
        [property: JsonPropertyName("unitsFulfilled")]
        int UnitsFulfilled
    );
    
    public record Meta(
        [property: JsonPropertyName("total")] int Total,
        [property: JsonPropertyName("page")] int Page,
        [property: JsonPropertyName("limit")] int Limit
    );

    // Can we aggregate the response wrapper somehow? Should they be defined here or in models?
    private record WaypointResponseWrapper([property: JsonPropertyName("data")] Waypoint Data);
    private record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);
    private record ContractListResponseWrapper([property: JsonPropertyName("data")] Contracts[] Data);
    private record ContractResponseWrapper([property: JsonPropertyName("data")] Contract Data);
    private record SystemResponseWrapper([property: JsonPropertyName("data")] SystemDetails Data);
    private record ShipListResponseWrapper([property: JsonPropertyName("data")] Ship[] Data, [property: JsonPropertyName("meta")] Meta Meta);


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

    public async Task<Contracts[]?> DeserializeContractList(Stream jsonStream) {
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

    public async Task<(Ship[]? Ships, Meta? MetaInfo)?> DeserializeShipList(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<ShipListResponseWrapper>(jsonStream, options);
            return response != null ? (response.Data, response.Meta) : null;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing ship list data: {ex.Message}");
            return null;
        }
    }

    public async Task<SystemDetails?> DeserializeSystem(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<SystemResponseWrapper>(jsonStream, options);
            return response?.Data;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing system data: {ex.Message}");
            return null;
        }
    }

    public async Task<Account?> DeserializeAccount(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            // PropertyNameCaseInsensitive = true, // Can be used, but JsonPropertyName is more explicit
        };
        try {
            var apiResponse = await JsonSerializer.DeserializeAsync<AccountResponseWrapper>(jsonStream, options);
            return apiResponse?.Data.AccountDetails; // Extracting the Account record
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing account data: {ex.Message}");
            return null;
        }
    }
}