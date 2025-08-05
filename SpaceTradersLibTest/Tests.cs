using SpaceTradersLib.Http;

namespace SpaceTradersLibTest;

[TestClass] // todo config file with parameters
public sealed class ShipTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestOrbitShip(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipOneShotAsync(shipSymbol, "orbit");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestDockShip(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipOneShotAsync(shipSymbol, "dock");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestRefuelShip(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipOneShotAsync(shipSymbol, "refuel");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestExtractShip(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipOneShotAsync(shipSymbol, "extract");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H48")]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H49")]
    public async Task TestNavigateShip(string shipSymbol, string navWaypoint) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
}

[TestClass]
public sealed class WaypointTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("X1-TT8", "shipyard")]
    public async Task TestSearchWaypointList(string systemSymbol, string trait) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var locationService = new LocationService(httpClient);
        var response = await locationService.GetWaypointListByTraitAsync(systemSymbol, trait);
        Assert.IsNotNull(response);
        foreach (var x in response) {
            TestContext.WriteLine(x.Symbol);
            TestContext.WriteLine(x.Type);
            TestContext.WriteLine("---------");
        }
    }
}