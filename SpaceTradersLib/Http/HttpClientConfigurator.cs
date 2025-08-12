using System.Net.Http.Headers;

namespace SpaceTradersLib.Http;

public class HttpClientConfigurator {
    public static void ConfigureDefaultClient(HttpClient httpClient) {
        var token = GetToken();
        if (token is null) throw new IOException("Token was null. Exiting.");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    private static string? GetToken() {
        try {
            using StreamReader reader = new("token.txt");
            string token = reader.ReadToEnd();
            return token;
        }
        catch (IOException ex) {
            Console.WriteLine($"File could not be read: {ex.Message}");
            return null;
        }
    }
}