using SpaceTradersLib.Http;

namespace SpaceTradersLibTest;

[TestClass]
public sealed class ShipTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestOrbitShipAsync(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.OrbitShipAsync(shipSymbol);
        TestContext.WriteLine(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestDockShipAsync(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.DockShipAsync(shipSymbol);
        TestContext.WriteLine(response);
        Assert.IsNotNull(response);
    }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H48")]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H49")]
    public async Task TestNavigateShipAsync(string shipSymbol, string navWaypoint) {
        var httpClient = BaseApiService.InitialiseHttpClient();
        var shipService = new ShipService(httpClient);
        var response = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
        TestContext.WriteLine(response);
        Assert.IsNotNull(response);
    }
}

[TestClass]
public class Test2 {
}