using SpaceTradersLib.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceTradersLib.Models;

namespace SpaceTradersLibTest;

[TestClass]
public static class TestGlobals
{
    public static string TestShip { get; private set; }
    public static string TestLoc { get; private set; }

    [AssemblyInitialize]
    public static async Task AssemblyInitialize(TestContext context)
    {
        TestShip = "BOUNDLESS_BEER-2";
        
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.GetShipAsync(TestShip);

        TestLoc = response?.Nav.WaypointSymbol;
    }
}

[TestClass]
public sealed class ShipTests {
    public TestContext TestContext { get; set; }
    
    private static async Task<string> GetShipLoc(string shipSymbol)
    {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.GetShipAsync(shipSymbol);
        Assert.IsNotNull(response);
        return response.Nav.WaypointSymbol;
    }
    
    [TestMethod]
    public async Task TestOrbitShip() {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        // Access global value
        var response = await shipService.ShipPostOneShotAsync(TestGlobals.TestShip, "orbit");
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
    [DataRow("BOUNDLESS_BEER-1")]
    public async Task TestChartShip(string shipSymbol) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.ShipPostOneShotAsync(shipSymbol, "chart");
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
    [DataRow("BOUNDLESS_BEER-2", "X1-US48-H56")]
    public async Task TestNavigateShip(string shipSymbol, string navWaypoint) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var shipService = new ShipService(httpClient);
        var response = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response); 
        // Access global value
        await shipService.NavigateShipAsync(shipSymbol, TestGlobals.TestLoc);
    }
}

[TestClass]
public sealed class LocationTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("X1-DS2", "marketplace")]
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
    
    [TestMethod]
    public async Task TestGetMarket() {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var locationService = new LocationService(httpClient);
        var response = await locationService.GetMarketAsync(TestGlobals.TestLoc);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response.Symbol);
        foreach (var x in response.Imports) {
            TestContext.WriteLine(x.Symbol);
            TestContext.WriteLine(x.Description);
            TestContext.WriteLine("---");
        }
    }
}

[TestClass]
public sealed class ContractTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow("cmjpwqmug1fxmri6vwbi0xyt1")]
    public async Task TestGetContract(string contractID) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var contractService = new ContractService(httpClient);
        var response = await contractService.GetContractAsync(contractID);
        Assert.IsNotNull(response);
        TestContext.WriteLine(response.id);
        TestContext.WriteLine(response.terms.ToString());
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
        TestContext.WriteLine(response.contract.ToString());
    }
}

[TestClass]
public sealed class AgentTests {
    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task TestGetAgent() { // todo probably just define a new GetAgent for testing this
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        var agentService = new AgentService(httpClient);
        var response = await agentService.GetAgentAsync();
        Assert.IsNotNull(response);
        TestContext.WriteLine(response.Symbol);
    
    }
}

[TestClass]
public sealed class AccountTests {
    public TestContext TestContext { get; set; }

    [DataRow("BOUNDLESS_BEER", "ETHEREAL")]
    [TestMethod]
    public async Task TestRegisterAgent(string symbol, string faction) {
        var httpClient = new HttpClient();
        HttpClientConfigurator.ConfigureDefaultClient(httpClient);
        string accountToken = await File.ReadAllTextAsync("accountToken.txt");
        var accountService = new AccountService(httpClient);
        var agent = await accountService.RegisterAgent(faction, symbol, accountToken);
        Assert.IsNotNull(agent);
        TestContext.WriteLine(agent.token);
    }
}