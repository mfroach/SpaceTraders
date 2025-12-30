using System.Text;
using System.Text.Json;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public abstract class BaseApiService(HttpClient httpClient)
{
    protected readonly HttpClient HttpClient = httpClient;
    protected readonly Deserializer Deserializer = new();

    protected async Task<Result<TSuccess?>> GetAsync<TSuccess>(string endpoint) where TSuccess : class
    {
        try
        {
            using var response = await HttpClient.GetAsync($"https://api.spacetraders.io/v2/{endpoint}",
                HttpCompletionOption.ResponseHeadersRead);

            var stream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = await Deserializer.DeserializeAsync<TSuccess>(stream);
                return Result<TSuccess>.Success(data);
            }

            var error = await Deserializer.DeserializeApiErrorAsync(stream);
            return Result<TSuccess>.Failure(error);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return Result<TSuccess>.Failure(new(123, ex.Message, 0, "0"));
        }
    }
    
    protected async Task<Result<TSuccess?>> PostAsync<TSuccess>(string endpoint, HttpContent? payload) where TSuccess : class
    {
        try
        {
            using var response = await HttpClient.PostAsync($"https://api.spacetraders.io/v2/{endpoint}", payload);

            var stream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = await Deserializer.DeserializeAsync<TSuccess>(stream);
                return Result<TSuccess>.Success(data);
            }

            var error = await Deserializer.DeserializeApiErrorAsync(stream);
            return Result<TSuccess>.Failure(error);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            return Result<TSuccess>.Failure(new(1234, ex.Message, 0, "0"));
        }
    }

    protected async Task<HttpResponseMessage?> Post(string endpoint, HttpContent payload)
    {
        try
        {
            var response = await HttpClient.PostAsync(new Uri($"https://api.spacetraders.io/v2/{endpoint}"), payload);
            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP POST request failed: {ex.Message}");
            return null;
        }
    }

    internal HttpContent RequestBuilder<TData>(TData content)
    {
        // todo this is returning malformed data in some cases, pretty sure
        var json = JsonSerializer.Serialize<TData>(content);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}