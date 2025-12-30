using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class AgentService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<Result<Agent?>> GetAgentAsync()
    {
        return await GetAsync<Agent>("my/agent");
    }
}