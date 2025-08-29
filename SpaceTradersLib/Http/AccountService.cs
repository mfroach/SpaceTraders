using System.Net.Http.Headers;
using System.Net.Http.Json;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public class AccountService(HttpClient httpClient) : BaseApiService(httpClient) {

    public async Task<string> RegisterAgent(string symbol, string faction, string accountToken) {
        RegisterAgentData data = new RegisterAgentData(symbol, faction);
        var payload = RequestBuilder(data);
        Uri endpoint = new Uri("https://api.spacetraders.io/v2/register");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accountToken); // todo what is scope?
        var deserializer = new Deserializer();
        try {
            using var responseMessage =
                await HttpClient.PostAsJsonAsync(endpoint, payload);
            if (responseMessage.IsSuccessStatusCode) {
                Console.WriteLine(responseMessage);
                return deserializer.DeserializeRegisterAgent(responseMessage).token;
            } else {
                Console.WriteLine(responseMessage.StatusCode + " | " + responseMessage.ReasonPhrase);
                return null;
            }
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to register agent failed: {ex.Message}");
            return null;
        } 
    }

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