using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class ContractService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Result<Contract[]?>> GetContractListAsync() {
        return await GetAsync<Contract[]>("my/contracts");
    }

    public async Task<Result<Contract?>> GetContractAsync(string contractId) {
        return await GetAsync<Contract>($"my/contracts/{contractId}");
    }

    public async Task<ContractAccept?> AcceptContractAsync(string contractId) {
        try {
            var response = await HttpClient.PostAsync(
                $"https://api.spacetraders.io/v2/my/contracts/{contractId}/accept", 
                null
            );
            response.EnsureSuccessStatusCode();
        
            await using var stream = await response.Content.ReadAsStreamAsync();
            var result = await Deserializer.DeserializeContractAccept(stream);
            return result;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to accept contract failed: {ex.Message}");
            return null;
        }
    }
}