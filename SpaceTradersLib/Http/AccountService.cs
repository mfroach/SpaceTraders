using System.Net.Http.Headers;
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
                await HttpClient.PostAsync(endpoint, payload);
            if (responseMessage.IsSuccessStatusCode) {
                Console.WriteLine(responseMessage);
                return deserializer.DeserializeRegisterAgent(responseMessage).token; // todo succeeds in registering, fails in deserialize
            } else {
                //return deserializer.DeserializeError(responseMessage).error.ToString(); doesn't work and doesn't throw stack trace?
                throw new NotImplementedException("Problem posting or deseralizing. Can't deserialize error. sorry");
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