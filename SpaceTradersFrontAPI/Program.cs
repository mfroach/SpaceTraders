using Microsoft.AspNetCore.Mvc;
using SpaceTradersFrontAPI.Mapping;
using SpaceTradersLib.Http;

namespace SpaceTradersFrontAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSvelteApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        builder.Services.ConfigureHttpClientDefaults(httpClientBuilder => {
            httpClientBuilder.ConfigureHttpClient(HttpClientConfigurator.ConfigureDefaultClient);
        });
        builder.Services.AddHttpClient<AgentService, AgentService>();
        builder.Services.AddHttpClient<LocationService, LocationService>();
        builder.Services.AddHttpClient<ShipService, ShipService>();
        builder.Services.AddHttpClient<ContractService, ContractService>();
        builder.Services.AddHttpClient<FactionService, FactionService>();
        var app = builder.Build();

        app.MapGroup("/my").MapMyAPI();
        app.MapGroup("/ships").MapShipsAPI();
        app.MapGroup("/systems").MapLocationAPI();
        app.MapGroup("/contracts").MapContractsAPI();
        app.MapGroup("/factions").MapFactionsAPI();
        app.MapPost("/register", (string symbol, string faction, string accountToken, [FromServices] AccountService accountService) => 
            accountService.RegisterAgent(symbol, faction, accountToken));
        app.UseCors("AllowSvelteApp");
        app.Run();
    }
}

public static class RouteGroupExtensions {
    public static RouteGroupBuilder MapMyAPI(this RouteGroupBuilder group) {
        group.MapGet("/agent", async ([FromServices] AgentService agentService) => {
            var agent = await agentService.GetAgentAsync();
            if (agent is null) { return Results.InternalServerError("Response was bad. Token may be invalid."); }
            var dto = agent.ToDto();
            return Results.Ok(dto);
        });
        return group;
    }

    public static RouteGroupBuilder MapShipsAPI(this RouteGroupBuilder group) {
        // get all ships
        group.MapGet("/",
            async ([FromServices] ShipService shipService) => {
                var ships = await shipService.GetShipListAsync();
                if (ships is null) {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }
                var dtos = ships.Select(s => s.ToDto()).ToArray();
                return Results.Ok(dtos);
            });

        // get ship info by symbol
        group.MapGet("/{shipSymbol}",
            async (string shipSymbol, [FromServices] ShipService shipService) => {
                var ship = await shipService.GetShipAsync(shipSymbol);
                if (ship is null) {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }
                var dto = ship.ToDto();
                return Results.Ok(dto);
            });

        group.MapGet("/{shipSymbol}/{oneShot}",
            async (string shipSymbol, string oneShot, [FromServices] ShipService shipService) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, oneShot);
                if (result is null) {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }
                if (result.Contains("\"error\"")) {
                    return Results.BadRequest(result);
                }
                return Results.Ok(result);
            }); // known endpoints: orbit dock warp scrap repair siphon survey scan/systems scan/waypoints extract negotiate/contract chart

        group.MapGet("/{shipSymbol}/orbit",
            async (string shipSymbol, [FromServices] ShipService shipService) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, "orbit");
                if (result is null) {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }
                return Results.Ok(result);
            });

        group.MapGet("/{shipSymbol}/navigate/{navWaypoint}",
            async (string shipSymbol, string navWaypoint, [FromServices] ShipService shipService) => {
                var result = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
                if (result is null) {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }

                return Results.Ok(result);
            });

        group.MapPost("/{shipSymbol}/purchase", async (string shipSymbol, string tradeSymbol, int units, [FromServices] ShipService shipService) => {
            var result = await shipService.PurchaseCargoAsync(shipSymbol, tradeSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{shipSymbol}/sell", async (string shipSymbol, string tradeSymbol, int units, [FromServices] ShipService shipService) => {
            var result = await shipService.SellCargoAsync(shipSymbol, tradeSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{shipSymbol}/jettison", async (string shipSymbol, string tradeSymbol, int units, [FromServices] ShipService shipService) => {
            var result = await shipService.JettisonCargoAsync(shipSymbol, tradeSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{shipSymbol}/transfer", async (string shipSymbol, string tradeSymbol, int units, string targetShipSymbol, [FromServices] ShipService shipService) => {
            var result = await shipService.TransferCargoAsync(shipSymbol, tradeSymbol, units, targetShipSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{shipSymbol}/refuel", async (string shipSymbol, int? units, [FromServices] ShipService shipService) => {
            var result = await shipService.RefuelShipAsync(shipSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{shipSymbol}/jump", async (string shipSymbol, string systemSymbol, [FromServices] ShipService shipService) => {
            var result = await shipService.JumpShipAsync(shipSymbol, systemSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/purchase", async ([FromBody] PurchaseShipRequest request, [FromServices] ShipService shipService) => {
            var result = await shipService.PurchaseShipAsync(request.ShipType, request.WaypointSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        return group;
    }

    public record PurchaseShipRequest(string ShipType, string WaypointSymbol);
    
    public static RouteGroupBuilder MapLocationAPI(this RouteGroupBuilder group) {
        group.MapGet("/", async (int page, int limit, [FromServices] LocationService locationService) => {
            var systems = await locationService.GetSystemsAsync(page, limit);
            return systems is null ? Results.InternalServerError() : Results.Ok(systems);
        });

        group.MapGet("/{systemSymbol}", async (string systemSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetSystemAsync(systemSymbol);
            if (result is null) {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });

        group.MapGet("/{systemSymbol}/waypoints", async (string systemSymbol, string? traits, [FromServices] LocationService locationService) => {
            if (string.IsNullOrEmpty(traits)) {
                return Results.BadRequest("traits parameter is required");
            }

            var result = await locationService.GetWaypointListByTraitAsync(systemSymbol, traits);
            if (result is null) {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dtos = result.Select(w => w.ToDto()).ToArray();
            return Results.Ok(dtos);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetWaypointAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/market", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetMarketAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/shipyard", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetShipyardAsync(systemSymbol + "-" + waypointSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/jump-gate", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetJumpGateAsync(systemSymbol + "-" + waypointSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/construction", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetConstructionAsync(systemSymbol + "-" + waypointSymbol);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{systemSymbol}/waypoints/{waypointSymbol}/construction/supply", async (string systemSymbol, string waypointSymbol, string shipSymbol, string tradeSymbol, int units, [FromServices] LocationService locationService) => {
            var result = await locationService.SupplyConstructionAsync(systemSymbol + "-" + waypointSymbol, shipSymbol, tradeSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        return group;
    }
    
    public static RouteGroupBuilder MapContractsAPI(this RouteGroupBuilder group) {
        group.MapGet("/", async ([FromServices] ContractService contractService) => {
            var contracts = await contractService.GetContractsAsync();
            return contracts is null ? Results.InternalServerError() : Results.Ok(contracts);
        });

        group.MapGet("/{contractId}", async (string contractId, [FromServices] ContractService contractService) => {
            var contract = await contractService.GetContractAsync(contractId);
            return contract is null ? Results.InternalServerError() : Results.Ok(contract);
        });

        group.MapPost("/{contractId}/accept", async (string contractId, [FromServices] ContractService contractService) => {
            var result = await contractService.AcceptContractAsync(contractId);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{contractId}/deliver", async (string contractId, string shipSymbol, string tradeSymbol, int units, [FromServices] ContractService contractService) => {
            var result = await contractService.DeliverContractAsync(contractId, shipSymbol, tradeSymbol, units);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        group.MapPost("/{contractId}/fulfill", async (string contractId, [FromServices] ContractService contractService) => {
            var result = await contractService.FulfillContractAsync(contractId);
            return result is null ? Results.InternalServerError() : Results.Ok(result);
        });

        return group;
    }

    public static RouteGroupBuilder MapFactionsAPI(this RouteGroupBuilder group) {
        group.MapGet("/", async (int page, int limit, [FromServices] FactionService factionService) => {
            var factions = await factionService.GetFactionsAsync(page, limit);
            return factions is null ? Results.InternalServerError() : Results.Ok(factions);
        });

        group.MapGet("/{factionSymbol}", async (string factionSymbol, [FromServices] FactionService factionService) => {
            var faction = await factionService.GetFactionAsync(factionSymbol);
            return faction is null ? Results.InternalServerError() : Results.Ok(faction);
        });

        return group;
    }
}