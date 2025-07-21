using System.Text.Json;
using SpaceTraders.Models;

namespace SpaceTraders.Services;

public class Deserializer {
    private async Task<TData?> DeserializeInternal<TWrapper, TData>(
        Stream jsonStream,
        Func<TWrapper, TData> dataSelector) {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        try {
            var wrapper = await JsonSerializer.DeserializeAsync<TWrapper>(jsonStream, options);
            return wrapper is not null ? dataSelector(wrapper) : default;
        }
        catch (JsonException ex) {
            Console.WriteLine($"Error deserializing data: {ex.Message}");
            return default;
        }
    }

    public Task<Agent?> DeserializeAgent(Stream jsonStream) =>
        DeserializeInternal<AgentResponseWrapper, Agent?>(jsonStream, r => r.Data);

    public Task<Waypoint[]?> DeserializeWaypointList(Stream jsonStream) =>
        DeserializeInternal<WaypointListResponseWrapper, Waypoint[]?>(jsonStream, r => r.Data);

    public Task<Waypoint?> DeserializeWaypoint(Stream jsonStream) =>
        DeserializeInternal<WaypointResponseWrapper, Waypoint?>(jsonStream, r => r.Data);

    public Task<Contract[]?> DeserializeContractList(Stream jsonStream) =>
        DeserializeInternal<ContractListResponseWrapper, Contract[]>(jsonStream, r => r.Data);
    
    public Task<Contract?> DeserializeContract(Stream jsonStream) =>
        DeserializeInternal<ContractResponseWrapper, Contract>(jsonStream, r => r.Data);

    public Task<Ship[]?> DeserializeShipList(Stream jsonStream) =>
        DeserializeInternal<ShipListResponseWrapper, Ship[]>(jsonStream, r => r.Data);
    
    public Task<Ship?> DeserializeShip(Stream jsonStream) =>
        DeserializeInternal<ShipResponseWrapper, Ship>(jsonStream, r => r.Data);

    public Task<Ship?> DeserializeShipStatus(HttpResponseMessage httpResponseMessage) =>
        throw new NotImplementedException();
    
    public Task<SystemDetails?> DeserializeSystem(Stream jsonStream) =>
        DeserializeInternal<SystemResponseWrapper, SystemDetails>(jsonStream, r => r.Data);
    
    public Task<Account?> DeserializeAccount(Stream jsonStream) =>
        DeserializeInternal<AccountResponseWrapper, Account>(jsonStream, r => r.Data.AccountDetails);
}