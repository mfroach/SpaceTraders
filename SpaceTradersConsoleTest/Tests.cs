using SpaceTraders;
using SpaceTraders.Http;

namespace SpaceTradersConsoleTest;

[TestClass]
public sealed class ShipTests {
    private string token =
        "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZGVudGlmaWVyIjoiQk9VTkRMRVNTX0JFRVIiLCJ2ZXJzaW9uIjoidjIuMy4wIiwicmVzZXRfZGF0ZSI6IjIwMjUtMDctMjAiLCJpYXQiOjE3NTMxMjMwNDIsInN1YiI6ImFnZW50LXRva2VuIn0.YpfKXCdPkELGRV1h-di1hXLMdlcSG2XRTSQWUycuNZFzsPkyp8Q3CpUyZksnpkeq7PTldmlan5oipH8SIsLm0tN6Qdn1hLcHVqbhysRBwEcxOLkTh0XpaCFfryTZaUKGiTarVOALpMkSYsGhRwSsIwJp3HBoRGtfkdROpJtWOFVOaYz3wimBOMt_eyNCqDaobRGmGtGE6GLhVbASoc00HxwRUnF2BW-z4-xJVLRzkWe9G9bfKdmGVCY0LKkVrikN9qW3Xel6idCPHKMoEKDqEvwbFBJy68OuE2DxIZ1KwaH-Ff55mL6P7nS0iYn831xhuQ4Oqgo_gN_2_R8ENGZr1T26zbM4xk_-7_WXIFa-qMUY8XzCIOArxQR_oHFew7PMK3zDMZG6aB9zGhOVv5t6kbftW9R79IkJhx75pjd_UVJP3Roe3ApfmBSgPxeMJme1AG1T3XeEKa-YmKOt7nQD4jGPodMZQ_pODdmnKemcPX9wnbez1E2qRU1gppJRQpC3pd0gcOc7Bl00cjHzqj-LgKXi2W3YjaLs61ktIxeIss5vmMCvIum7DM1ULicvTtmOktGZDs3MHP79Jc3GAbEBJVBVM8jK9EDgcMRigmof-Dd_Xe0XOQt3NVtfm_y67bAawFlgjoP9wMv4OG5TcdJs63gXQOgOpHXEOsVHZrwoyGw";
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestOrbitShipAsync(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient(token);
        var shipService = new ShipService(httpClient);
        var response = await shipService.OrbitShipAsync(shipSymbol);
        Assert.IsNotNull(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2")]
    public async Task TestDockShipAsync(string shipSymbol) {
        var httpClient = BaseApiService.InitialiseHttpClient(token);
        var shipService = new ShipService(httpClient);
        var response = await shipService.DockShipAsync(shipSymbol);
        Assert.IsNotNull(response);
    }
    
    [TestMethod]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H48")]
    [DataRow("BOUNDLESS_BEER-2", "X1-BH6-H49")]
    public async Task TestNavigateShipAsync(string shipSymbol, string navWaypoint) {
        var httpClient = BaseApiService.InitialiseHttpClient(token);
        var shipService = new ShipService(httpClient);
        var response = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
        Assert.IsNotNull(response);
    }
}

[TestClass]
public sealed class Test2 {}