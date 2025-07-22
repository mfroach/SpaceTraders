using SpaceTradersLib.Models;

namespace SpaceTradersLib.Http;

public class ContractService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Contract[]?> GetContractListAsync() {
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync("https://api.spacetraders.io/v2/my/contracts");
            return await Deserializer.DeserializeContractList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch contract data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Contract?> GetContractAsync(string contractId) {
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/contracts/{contractId}");
            return await Deserializer.DeserializeContract(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch contract data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<HttpResponseMessage> AcceptContractAsync(string contractId) {
        return await HttpClient.PostAsync($"https://api.spacetraders.io/v2/my/contracts/{contractId}/accept", null);
    }
}