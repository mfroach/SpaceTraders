using SpaceTraders.Models;
using SpaceTraders.Services;
namespace SpaceTraders.Http;

public class AccountService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Account?> GetAccountAsync() {
        var deserializer = new Deserializer();
        try {
            using var jsonStream = await HttpClient.GetStreamAsync("https://api.spacetraders.io/v2/my/account");
            return await deserializer.DeserializeAccount(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch account data failed: {ex.Message}");
            return null;
        }
    }
}