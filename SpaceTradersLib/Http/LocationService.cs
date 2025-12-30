using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class LocationService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<Result<SystemDetails?>> GetSystemAsync(string system) {
        return await GetAsync<SystemDetails>($"systems/{system}");
    }

    public async Task<Result<Waypoint?>> GetWaypointAsync(string waypoint) {
        string system = waypoint[..7];
        return await GetAsync<Waypoint>($"systems/{system}/waypoints/{waypoint}");
    }
    
    public async Task<Result<Waypoint[]?>> GetWaypointListByTraitAsync(string searchSystem, string searchTrait) {
        return await GetAsync<Waypoint[]>($"systems/{searchSystem}/waypoints?traits={searchTrait.ToUpperInvariant()}");
    }
    
    public async Task<Result<Market?>> GetMarketAsync(string waypoint) {
        string system = waypoint[..7];
        return await GetAsync<Market>($"systems/{system}/waypoints/{waypoint}/market");
    }
    
}