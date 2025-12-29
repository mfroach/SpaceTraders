using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class LocationService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<SystemDetails?> GetSystemAsync(string system) {
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}");
            return await Deserializer.DeserializeSystem(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Waypoint?> GetWaypointAsync(string waypoint) {
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}");
            return await Deserializer.DeserializeWaypoint(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Waypoint[]?> GetWaypointListByTraitAsync(string searchSystem, string searchTrait) {
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{searchSystem}/waypoints?traits={searchTrait.ToUpperInvariant()}");
            return await Deserializer.DeserializeWaypointList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location list data failed: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Market?> GetMarketAsync(string waypoint) {
        string system = waypoint[..7];
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/systems/{system}/waypoints/{waypoint}/market");
            return await Deserializer.DeserializeMarket(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch location data failed: {ex.Message}");
            return null;
        }
    }
    
}