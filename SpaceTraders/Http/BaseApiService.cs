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
}