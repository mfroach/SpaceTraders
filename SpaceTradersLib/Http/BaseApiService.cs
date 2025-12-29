using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public abstract class BaseApiService(HttpClient httpClient) {
    protected readonly HttpClient HttpClient = httpClient;
    protected readonly Deserializer Deserializer = new();

    protected async Task<Result<TSuccess>> GetAsync<TSuccess>(string endpoint) where TSuccess : class {
        try {
            using var response = await HttpClient.GetAsync($"https://api.spacetraders.io/v2/{endpoint}",
                HttpCompletionOption.ResponseHeadersRead);

            var stream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode) {
                var data = await Deserializer.DeserializeAsync<TSuccess>(stream);
                return Result<TSuccess>.Success(data);
            }

            var error = await Deserializer.DeserializeAsync<Error>(stream);
            return Result<TSuccess>.Failure(error);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return Result<TSuccess>.Failure(new Error { Message = ex.Message });
        }
    }

    protected async Task<HttpResponseMessage?> Post(string endpoint, HttpContent payload) {
        try {
            var response = await HttpClient.PostAsync(new Uri($"https://api.spacetraders.io/v2/{endpoint}"), payload);
            return response;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP POST request failed: {ex.Message}");
            return null;
        }
    }

    internal HttpContent RequestBuilder<TData>(TData content) {
        // todo this is returning malformed data, pretty sure
        var json = JsonSerializer.Serialize<TData>(content);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}