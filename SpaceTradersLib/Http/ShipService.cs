using System.Net.Http.Json;
using System.Text;
using SpaceTradersLib.Models;
using SpaceTradersLib.Services;

namespace SpaceTradersLib.Http;

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

    public async Task<string?> NavigateShipAsync(string shipSymbol, string navWaypoint) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/navigate");
        NavData navData = new NavData(navWaypoint);
        var deserializer = new Deserializer();
        var navPayload = RequestBuilder(navData);
        var payloadString = await navPayload.ReadAsStringAsync();
        try {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, new StringContent(payloadString, Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode) {
                return await responseMessage.Content.ReadAsStringAsync();
            } else {
                var errorBody = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"Navigate failed: {responseMessage.StatusCode} - {errorBody}");
                return errorBody;
            }
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to navigate {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

/*    public async Task<ShipScanList?> ScanShipsAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/scan/ships");
        ShipSymbolData shipSymbolData = new ShipSymbolData(shipSymbol);
        try {
            using var responseMessage =
                await HttpClient.PostAsJsonAsync(endpoint, shipSymbolData);
            if (responseMessage.IsSuccessStatusCode) {
                Console.WriteLine(responseMessage);
                return deserializer.DeserializeShipScanList(responseMessage.Content);
            } else {
                return responseMessage.StatusCode + " | " + responseMessage.ReasonPhrase;
            }
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to scan from {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }
*/
    public async Task<string?> WarpShipAsync(string shipSymbol, string navWaypoint) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/warp");
        NavData navData = new NavData(navWaypoint);
        var navPayload = RequestBuilder(navData);
        try {
            using var responseMessage =
                await HttpClient.PostAsync(endpoint, navPayload);
            Console.WriteLine(responseMessage);
            return await responseMessage.Content.ReadAsStringAsync(); // should return same schema as NavigateShipAsync
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to navigate {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string> ShipPostOneShotAsync(string shipSymbol, string endpoint) {
        Uri path = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/{endpoint}");
        try {
            using var responseMessage =
                await HttpClient.PostAsync(path, null);
            Console.WriteLine(responseMessage.StatusCode);
            return await responseMessage.Content.ReadAsStringAsync(); // todo / JsonTypeDetector to deserialize by TData
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to {endpoint} {shipSymbol} failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> PurchaseCargoAsync(string shipSymbol, string tradeSymbol, int units) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/purchase");
        var payload = RequestBuilder(new { symbol = tradeSymbol, units });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to purchase cargo failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> SellCargoAsync(string shipSymbol, string tradeSymbol, int units) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/sell");
        var payload = RequestBuilder(new { symbol = tradeSymbol, units });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to sell cargo failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> JettisonCargoAsync(string shipSymbol, string tradeSymbol, int units) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/jettison");
        var payload = RequestBuilder(new { symbol = tradeSymbol, units });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to jettison cargo failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> TransferCargoAsync(string shipSymbol, string tradeSymbol, int units, string targetShipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/transfer");
        var payload = RequestBuilder(new { tradeSymbol, units, shipSymbol = targetShipSymbol });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to transfer cargo failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> RefuelShipAsync(string shipSymbol, int? units = null) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/refuel");
        var payload = units.HasValue ? RequestBuilder(new { units = units.Value }) : null;
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to refuel ship failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> PurchaseShipAsync(string shipType, string waypointSymbol) {
        Uri endpoint = new Uri("https://api.spacetraders.io/v2/my/ships");
        var payload = RequestBuilder(new { shipType, waypointSymbol });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to purchase ship failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> JumpShipAsync(string shipSymbol, string systemSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/jump");
        var payload = RequestBuilder(new { waypointSymbol = systemSymbol });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to jump ship failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> ChartWaypointAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/chart");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to chart waypoint failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> ScanShipsAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/scan/ships");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to scan ships failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> ScanSystemsAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/scan/systems");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to scan systems failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> ScanWaypointsAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/scan/waypoints");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to scan waypoints failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> RepairShipAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/repair");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to repair ship failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> ScrapShipAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/scrap");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to scrap ship failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> SiphonResourcesAsync(string shipSymbol) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/siphon");
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, null);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to siphon resources failed: {ex.Message}");
            return null;
        }
    }

    public async Task<string?> RefineResourcesAsync(string shipSymbol, string produce) {
        Uri endpoint = new Uri($"https://api.spacetraders.io/v2/my/ships/{shipSymbol}/refine");
        var payload = RequestBuilder(new { produce });
        try {
            using var responseMessage = await HttpClient.PostAsync(endpoint, payload);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) {
            Console.WriteLine($"HTTP post to refine resources failed: {ex.Message}");
            return null;
        }
    }
}