using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SpaceTraders.Models;

namespace SpaceTraders.Services;

public class Deserializer { // todo move all records to respective model files
    
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
            PropertyNameCaseInsensitive = true
        };
        try {
            var apiResponse = await JsonSerializer.DeserializeAsync<AccountResponseWrapper>(jsonStream, options);
            return apiResponse?.Data.AccountDetails;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing account data: {ex.Message}");
            return null;
        }
    }
}