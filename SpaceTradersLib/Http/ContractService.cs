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

    public async Task<Contract[]?> GetContractsAsync() => await GetContractListAsync();

    public async Task<string?> DeliverContractAsync(string contractId, string shipSymbol, string tradeSymbol, int units) {
        var payload = RequestBuilder(new { shipSymbol, tradeSymbol, units });
        try {
            using var responseMessage = await HttpClient.PostAsync(
                $"https://api.spacetraders.io/v2/my/contracts/{contractId}/deliver",
                payload
            );
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to deliver contract failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> FulfillContractAsync(string contractId) {
        try {
            using var responseMessage = await HttpClient.PostAsync(
                $"https://api.spacetraders.io/v2/my/contracts/{contractId}/fulfill",
                null
            );
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fulfill contract failed: {ex.Message}");
            return null;
        }
    }
}