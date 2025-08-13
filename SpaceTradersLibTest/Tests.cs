using SpaceTradersLib.Http;
using SpaceTradersLib.Services;

namespace SpaceTradersLibTest;

[TestClass] // todo config file with parameters
public sealed class ShipTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestOrbitShip(string shipSymbol) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipPostOneShotAsync(shipSymbol, "orbit");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }

    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestDockShip(string shipSymbol) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipPostOneShotAsync(shipSymbol, "dock");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestRefuelShip(string shipSymbol) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipPostOneShotAsync(shipSymbol, "refuel");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestExtractShip(string shipSymbol) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipPostOneShotAsync(shipSymbol, "extract");
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H48")]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H49")]
    public async Task TestNavigateShip(string shipSymbol, string navWaypoint) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
}

[TestClass]
public sealed class LocationTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("X1-TT8", "shipyard")]
    public async Task TestSearchWaypointList(string systemSymbol, string trait) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
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

[TestClass]
public sealed class ContractTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("123")]
    public async Task TestGetContract(string contractID) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var contractService = new ContractService(httpClient);
        var response = await contractService.GetContractAsync(contractID);
        Assert.IsNotNull(response);
        
    }
    
    [TestMethod]
    public async Task TestGetContractList() {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var contractService = new ContractService(httpClient);
        var response = await contractService.GetContractListAsync();
        Assert.IsNotNull(response);
        foreach (var x in response) {
            TestContext.WriteLine(x.id);
            TestContext.WriteLine(x.type);
            TestContext.WriteLine("--------");
        }
    }

    [TestMethod]
    [DataRow("cmea8iw1rqaowri74kk05q8ke")]
    public async Task TestAcceptContract(string contractID) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var contractService = new ContractService(httpClient);
        var response = await contractService.AcceptContractAsync(contractID);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response);
    }
}

[TestClass]
public sealed class AgentTests {
    [TestMethod]
    public async Task TestGetAgent() { // todo probably just define a new GetAgent for testing this
    /*    var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var agentService = new AgentService(httpClient);
        var response = await agentService.GetAgentAsync();
        var deserializer = new Deserializer();
        var data = deserializer.Parse(response);
        Assert.IsNotNull(response);
        TestContext.WriteLine(data);
    */
    }
}