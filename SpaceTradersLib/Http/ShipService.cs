using System.Net.Http.Json;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

public class ShipService(HttpClient httpClient) : BaseApiService(httpClient)
{
    private Deserializer deserializer = new Deserializer();

    public async Task<Ship[]?> GetShipListAsync()
    {
        try
        {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships");
            return await deserializer.DeserializeShipList(jsonStream);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Ship?> GetShipAsync(string shipSymbol)
    {
        try
        {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}");
            return await deserializer.DeserializeShip(jsonStream);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> NavigateShipAsync(string shipSymbol, string navWaypoint)
    {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/navigate");
        NavData navData = new NavData(navWaypoint);
        var navPayload = RequestBuilder(navData);
        try
        {
            using var responseMessage =
                await HttpClient.PostAsJsonAsync(endpoint, navPayload);
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine(responseMessage);
                return await responseMessage.Content.ReadAsStringAsync(); // todo deserialize and create DTO
            }
            else
            {
                return responseMessage.StatusCode + " | " + responseMessage.ReasonPhrase;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP post to navigate {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<ShipScanList?> ScanShipsAsync(string shipSymbol)
    {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/scan/ships");
        ShipSymbolData shipSymbolData = new ShipSymbolData(shipSymbol);
        try
        {
            using var responseMessage =
                await HttpClient.PostAsJsonAsync(endpoint, shipSymbolData);
            await using var stream = await responseMessage.Content.ReadAsStreamAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine(responseMessage);
                return await deserializer.DeserializeShipScanList(stream);
            }
            else
            {
                Console.WriteLine(responseMessage.StatusCode + " | " + responseMessage.ReasonPhrase);
                //return await Deserializer.DeserializeError(stream); TODO implement Result pattern
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP post to scan from {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> WarpShipAsync(string shipSymbol, string navWaypoint)
    {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/warp");
        NavData navData = new NavData(navWaypoint);
        var navPayload = RequestBuilder(navData);
        try
        {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, navPayload);
            Console.WriteLine(responseMessage);
            return await responseMessage.Content.ReadAsStringAsync(); // should return same schema as NavigateShipAsync
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP post to navigate {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> ShipPostOneShotAsync(string shipSymbol, string endpoint)
    {
        Uri path = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/{endpoint}");
        try
        {
            using var responseMessage =
                await HttpClient.PostAsync(path, null);
            Console.WriteLine(responseMessage.StatusCode);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP post to {endpoint} {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }
}