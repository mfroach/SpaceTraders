using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class ContractService(HttpClient httpClient) : BaseApiService(httpClient)
{
    public async Task<Result<Contract[]?>> GetContractListAsync()
    {
        return await GetAsync<Contract[]>("my/contracts");
    }

    public async Task<Result<Contract?>> GetContractAsync(string contractId)
    {
        return await GetAsync<Contract>($"my/contracts/{contractId}");
    }

    public async Task<Result<ContractAccept?>> AcceptContractAsync(string contractId)
    {
        var endpoint = $"my/contracts/{contractId}/accept";
        return await PostAsync<ContractAccept>(endpoint, null);
    }
}