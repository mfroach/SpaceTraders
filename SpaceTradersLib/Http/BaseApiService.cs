using System.Text;
using System.Text.Json;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public abstract class BaseApiService(HttpClient httpClient) {
    protected readonly HttpClient HttpClient = httpClient;
    protected readonly Deserializer Deserializer = new();

    protected async Task<Stream?> Get(string endpoint) {
        var uri = new Uri($"https://api.spacetraders.io/v2/{endpoint}");
        try {
            return await HttpClient.GetStreamAsync(uri);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP GET request failed: {ex.Message}");
            return null;
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