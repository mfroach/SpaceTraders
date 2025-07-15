using System.Net.Http.Headers;
using SpaceTraders.Services;

namespace SpaceTraders.Http;

public abstract class BaseApiService {
    protected readonly HttpClient HttpClient;
    protected readonly Deserializer Deserializer;

    protected BaseApiService(HttpClient httpClient) {
        HttpClient = httpClient;
        Deserializer = new Deserializer();
    }
    
    public static HttpClient InitialiseHttpClient(string token) {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    }

    protected async Task<Stream> Get(string endpoint) {
        var uri = new Uri($"https://api.spacetraders.io/v2/{endpoint}");
        try {
            return await HttpClient.GetStreamAsync(uri);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP GET request failed: {ex.Message}");
            return null;
        }
    }

    protected async Task<HttpResponseMessage> Post(string endpoint,HttpContent payload) {
        try {
            var response = await HttpClient.PostAsync(new Uri($"https://api.spacetraders.io/v2/{endpoint}"), payload);
            return response;
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP POST request failed: {ex.Message}");
            return null;
        }
    }

    internal HttpContent RequestBuilder(string requestType, string[]? content) {
        
        // todo take requestType and build object based on that type, define like records in Models, or constructors?
        // should content be array?
        
        throw new NotImplementedException();
    }
}