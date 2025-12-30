using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class ShipService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<Result<Ship[]?>> GetShipListAsync()
    {
        return await GetAsync<Ship[]>("my/ships");
    }

    public async Task<Result<Ship?>> GetShipAsync(string shipSymbol)
    {
        return await GetAsync<Ship>($"my/ships/{shipSymbol}");
    }

    public async Task<Result<string?>> NavigateShipAsync(string shipSymbol, string navWaypoint)
    {
        var endpoint = $"my/ships/{shipSymbol}/navigate";
        NavData navData = new NavData(navWaypoint);
        var navPayload = RequestBuilder(navData);
        return await PostAsync<string>(endpoint, navPayload);
    }

    public async Task<Result<ShipScanList?>> ScanShipsAsync(string shipSymbol)
    {
        var endpoint = $"my/ships/{shipSymbol}/scan/ships";
        ShipSymbolData shipSymbolData = new(shipSymbol);
        var payload = RequestBuilder(shipSymbolData);
        
        return await PostAsync<ShipScanList>(endpoint, payload);
        
    }

    public async Task<Result<string?>> WarpShipAsync(string shipSymbol, string navWaypoint)
    {
        var endpoint = $"my/ships/{shipSymbol}/warp";
        NavData navData = new NavData(navWaypoint);
        var navPayload = RequestBuilder(navData);
        return await PostAsync<string>(endpoint, navPayload);
    }

    public async Task<Result<string?>> ShipPostOneShotAsync(string shipSymbol, string endpoint)
    {
        var path = $"my/ships/{shipSymbol}/{endpoint}";
        return await PostAsync<string>(path, null);
    }
}