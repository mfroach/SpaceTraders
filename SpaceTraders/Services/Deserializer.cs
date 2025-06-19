using System.Text.Json;
using SpaceTraders.Models;

namespace SpaceTraders.Services;

public class Deserializer {

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

    public async Task<Ship[]?> DeserializeShipList(Stream jsonStream) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        try {
            var response = await JsonSerializer.DeserializeAsync<ShipListResponseWrapper>(jsonStream, options);
            return response != null ? (response.Data) : null;
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