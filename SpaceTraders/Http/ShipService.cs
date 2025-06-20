using SpaceTraders.Services;
using SpaceTraders.Models;

namespace SpaceTraders.Http;

public class ShipService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Ship[]?> GetShipListAsync() {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships");
            return await deserializer.DeserializeShipList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }
    
    public async Task<Ship?> GetShipAsync(string shipSymbol) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}");
            return await deserializer.DeserializeShip(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }
}