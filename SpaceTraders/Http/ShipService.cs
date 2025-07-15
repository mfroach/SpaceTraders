using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Schema;
using SpaceTraders.Services;
using SpaceTraders.Models;

namespace SpaceTraders.Http;

public class ShipService(HttpClient httpClient) : BaseApiService(httpClient) {
    public async Task<Ship[]?> GetShipListAsync() {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships");
            return await deserializer.DeserializeShipList(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<Ship?> GetShipAsync(string shipSymbol) {
        var deserializer = new Deserializer();
        try {
            await using var jsonStream =
                await HttpClient.GetStreamAsync($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}");
            return await deserializer.DeserializeShip(jsonStream);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP request to fetch ship data failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> OrbitShipAsync(string shipSymbol) {
        var deserializer = new Deserializer();
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/orbit");
        try {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, null);
            Console.WriteLine(responseMessage.StatusCode);
            return await responseMessage.Content.ReadAsStringAsync();
            //return await deserializer.DeserializeShipStatus(responseMessage);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to orbit {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> DockShipAsync(string shipSymbol) {
        var deserializer = new Deserializer();
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/dock");
        try {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, null);
            Console.WriteLine(responseMessage.StatusCode);
            return await responseMessage.Content.ReadAsStringAsync();
            //return await deserializer.DeserializeShipStatus(responseMessage);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to dock {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> NavigateShipAsync(string shipSymbol, string navWaypoint) {
        var deserializer = new Deserializer();
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/navigate");
        
        //  todo build navData object, which is just object with one field "waypointSymbol"
        //  then serialize navData to JSON, navPayload
        var navPayload = RequestBuilder("test", ["test","test2"]);
        
        try {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, navPayload);
            Console.WriteLine(responseMessage.StatusCode);
            return await responseMessage.Content.ReadAsStringAsync();
            //return await deserializer.DeserializeShipStatus(responseMessage);
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to dock {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> RefuelShipAsync(string shipSymbol) {
        throw new NotImplementedException();
    }
}