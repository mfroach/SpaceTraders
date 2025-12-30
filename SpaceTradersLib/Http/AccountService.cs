using System.Net.Http.Headers;
using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class AccountService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<Result<AgentRegistered?>> RegisterAgent(string faction, string symbol, string accountToken)
    {
        var email = "";
        RegisterAgentData data = new RegisterAgentData(faction, symbol, email);
        var payload = RequestBuilder<RegisterAgentData>(data);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accountToken); // todo what is scope?
        return await PostAsync<AgentRegistered>("register", payload);
    }

    public async Task<Result<Account?>> GetAccountAsync()
    {
        return await GetAsync<Account>("my/account");
    }
}