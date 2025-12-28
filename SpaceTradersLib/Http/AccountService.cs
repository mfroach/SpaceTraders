using System.Net.Http.Headers;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public class AccountService(HttpClient httpClient) : BaseApiService(httpClient) {

    public async Task<AgentRegistered?> RegisterAgent(string faction, string symbol, string accountToken)
    {
        var email = "";
        RegisterAgentData data = new RegisterAgentData(faction, symbol, email);
        var payload = RequestBuilder<RegisterAgentData>(data);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accountToken); // todo what is scope?
        try {
            var response =
                await HttpClient.PostAsync("https://api.spacetraders.io/v2/register", payload);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();
            var result = await Deserializer.DeserializeRegisterAgent(stream);
            return result;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to register agent failed: {ex.Message}");
            return null;
        } 
    }

    public async Task<Account?> GetAccountAsync() {
        try {
            using var jsonStream = await HttpClient.GetStreamAsync("https://api.spacetraders.io/v2/my/account");
            return await Deserializer.DeserializeAccount(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch account data failed: {ex.Message}");
            return null;
        }
    }
}