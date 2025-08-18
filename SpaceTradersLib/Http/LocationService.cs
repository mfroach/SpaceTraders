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
        string system = waypoint.Substring(0, 6);
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
        string system = waypoint.Substring(0, 6);
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
    
}