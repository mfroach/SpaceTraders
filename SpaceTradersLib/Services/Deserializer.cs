using System.Text.Json;
using SpaceTradersLib.Models;

namespace SpaceTradersLib.Services;

public class Deserializer {
    private readonly JsonSerializerOptions _options = 
        new() { PropertyNameCaseInsensitive = true };

    public async Task<TData?> DeserializeAsync<TData>(Stream jsonStream) {
        try {
            var document = await JsonDocument.ParseAsync(jsonStream);
            if (document.RootElement.TryGetProperty("data", out var dataElement)) {
                return JsonSerializer.Deserialize<TData>(
                    dataElement.GetRawText(), 
                    _options
                );
            }
            return default;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing data: {ex.Message}");
            return default;
        }
    }

    public async Task<ApiError?> DeserializeApiErrorAsync(Stream jsonStream) {
        try {
            var document = await JsonDocument.ParseAsync(jsonStream);
            if (document.RootElement.TryGetProperty("error", out var errorElement)) {
                return JsonSerializer.Deserialize<ApiError>(
                    errorElement.GetRawText(), 
                    _options
                );
            }
            return default;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing api error: {ex.Message}");
            return default;
        }
    }

    public Task<Error?> DeserializeError(Stream jsonStream) =>
        DeserializeAsync<Error>(jsonStream);
    
    public Task<Agent?> DeserializeAgent(Stream jsonStream) => 
        DeserializeAsync<Agent>(jsonStream);

    public Task<AgentRegistered?> DeserializeRegisterAgent(Stream jsonStream) =>
        DeserializeAsync<AgentRegistered>(jsonStream);

    public Task<Waypoint?> DeserializeWaypoint(Stream jsonStream) => 
        DeserializeAsync<Waypoint>(jsonStream);

    public Task<Waypoint[]?> DeserializeWaypointList(Stream jsonStream) => 
        DeserializeAsync<Waypoint[]>(jsonStream);

    public Task<Market?> DeserializeMarket(Stream jsonStream) => 
        DeserializeAsync<Market>(jsonStream);

    public Task<Contract?> DeserializeContract(Stream jsonStream) => 
        DeserializeAsync<Contract>(jsonStream);

    public Task<Contract[]?> DeserializeContractList(Stream jsonStream) => 
        DeserializeAsync<Contract[]>(jsonStream);

    public Task<Ship?> DeserializeShip(Stream jsonStream) => 
        DeserializeAsync<Ship>(jsonStream);

    public Task<Ship[]?> DeserializeShipList(Stream jsonStream) => 
        DeserializeAsync<Ship[]>(jsonStream);

    public Task<ShipScanList?> DeserializeShipScanList(Stream jsonStream) =>
        DeserializeAsync<ShipScanList>(jsonStream);

    public Task<SystemDetails?> DeserializeSystem(Stream jsonStream) => 
        DeserializeAsync<SystemDetails>(jsonStream);

    public Task<Account?> DeserializeAccount(Stream jsonStream) => 
        DeserializeAsync<Account>(jsonStream);

    public Task<ContractAccept?> DeserializeContractAccept(Stream jsonStream) =>
        DeserializeAsync<ContractAccept>(jsonStream);
}