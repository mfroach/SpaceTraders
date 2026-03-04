using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public class LocationService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<SystemDetails?> GetSystemAsync(string system) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}");
            return await deserializer.DeserializeSystem(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Waypoint?> GetWaypointAsync(string waypoint) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}");
            return await deserializer.DeserializeWaypoint(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Waypoint[]?> GetWaypointListByTraitAsync(string searchSystem, string searchTrait) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{searchSystem}/waypoints?traits={searchTrait.ToUpperInvariant()}");
            return await deserializer.DeserializeWaypointList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location list data failed: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Market?> GetMarketAsync(string waypoint) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/market");
            return await deserializer.DeserializeMarket(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Shipyard?> GetShipyardAsync(string waypoint) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/shipyard");
            var root = await deserializer.DeserializeGenericAsync<ShipyardRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch shipyard failed: {ex.Message}");
            return null;
        }
    }

    public async Task<JumpGate?> GetJumpGateAsync(string waypoint) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/jump-gate");
            var root = await deserializer.DeserializeGenericAsync<JumpGateRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch jump gate failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Construction?> GetConstructionAsync(string waypoint) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/construction");
            var root = await deserializer.DeserializeGenericAsync<ConstructionRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch construction failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> SupplyConstructionAsync(string waypoint, string shipSymbol, string tradeSymbol, int units) {
        var deserializer = new Deserializer();
        string system = waypoint[..7];
        var payload = RequestBuilder(new { shipSymbol, tradeSymbol, units });
        try {
            using var responseMessage = await HttpClient.PostAsync(
                $"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/construction/supply",
                payload
            );
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to supply construction failed: {ex.Message}");
            return null;
        }
    }

    public async Task<SystemDetails[]?> GetSystemsAsync(int page = 1, int limit = 20) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems?page={page}&limit={limit}");
            var root = await deserializer.DeserializeGenericAsync<SystemListRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch systems failed: {ex.Message}");
            return null;
        }
    }
    
}