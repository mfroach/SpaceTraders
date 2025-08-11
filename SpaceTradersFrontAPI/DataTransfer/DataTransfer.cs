using AutoMapper;
using SpaceTradersLib.Http;
using SpaceTradersLib.Models;
using SpaceTradersFrontAPI.Models;
namespace SpaceTradersFrontAPI.DataTransfer;

public class DataTransfer {
    static HttpClient httpClient = BaseApiService.InitialiseHttpClient();
    static LocationService locationService = new LocationService(httpClient);
    static AgentService agentService = new AgentService(httpClient);
    static AccountService accountService = new AccountService(httpClient);
    static ContractService contractService = new ContractService(httpClient);
    static ShipService shipService = new ShipService(httpClient);
}