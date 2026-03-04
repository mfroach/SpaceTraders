using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public class FactionService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<FactionDetails[]?> GetFactionsAsync(int page = 1, int limit = 20) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/factions?page={page}&limit={limit}");
            var root = await deserializer.DeserializeGenericAsync<FactionListRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch factions failed: {ex.Message}");
            return null;
        }
    }

    public async Task<FactionDetails?> GetFactionAsync(string factionSymbol) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/factions/{factionSymbol}");
            var root = await deserializer.DeserializeGenericAsync<FactionDetailsRoot>(jsonStream);
            return root?.data;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch faction failed: {ex.Message}");
            return null;
        }
    }
}
