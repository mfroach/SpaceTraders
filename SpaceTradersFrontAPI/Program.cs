using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceTradersFrontAPI.Database;
using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Http;
using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);
        builder.Services.ConfigureHttpClientDefaults(httpClientBuilder => {
            httpClientBuilder.ConfigureHttpClient(HttpClientConfigurator.ConfigureDefaultClient);
        });
        builder.Services.AddHttpClient<AgentService, AgentService>();
        builder.Services.AddHttpClient<LocationService, LocationService>();
        builder.Services.AddHttpClient<ShipService, ShipService>();
        var app = builder.Build();

        app.MapGroup("/my").MapMyAPI();
        app.MapGroup("/ships").MapShipsAPI();
        app.MapGroup("/systems").MapLocationAPI();
        app.MapPost("/register", (string symbol, string faction, string accountToken, [FromServices] AccountService accountService) => {
            return accountService.RegisterAgent(symbol, faction, accountToken); // todo cache in frontend
        });

        app.Run();
    }
}

public static class RouteGroupExtensions {
    public static RouteGroupBuilder MapMyAPI(this RouteGroupBuilder group) {
        group.MapGet("/agent", async ([FromServices] AgentService agentService, [FromServices] IMapper mapper) => {
            var agent = await agentService
                .GetAgentAsync();
            if (agent is null) {
                return Results.NoContent();
            }

            var dto = mapper.Map<AgentDTO>(agent);
            return Results.Ok(dto);
        });
        return group;
    }

    public static RouteGroupBuilder MapShipsAPI(this RouteGroupBuilder group) {
        // get ship info by symbol
        group.MapGet("/{shipSymbol}",
            async (string shipSymbol, [FromServices] ShipService shipService, [FromServices] IMapper mapper) => {
                var ship = await shipService.GetShipAsync(shipSymbol);
                if (ship is null) {
                    return Results.NoContent();
                }

                var dto = mapper.Map<ShipDTO>(ship);
                return Results.Ok(dto);
            });
        group.MapGet("/{shipSymbol}/{oneShot}",
            async (string shipSymbol, string oneShot, [FromServices] ShipService shipService, [FromServices] IMapper mapper) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, oneShot);
                if (result is null) {
                    return Results.NoContent();
                }
                return Results.Ok(result);
            }); // known endpoints: orbit dock warp scrap repair siphon survey scan/systems scan/waypoints extract negotiate/contract chart
        group.MapGet("/{shipSymbol}/orbit",
            async (string shipSymbol, [FromServices] ShipService shipService, [FromServices] IMapper mapper) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, "orbit");
                if (result is null) {
                    return Results.NoContent();
                }
                return Results.Ok(result);
            });
        group.MapGet("/{shipSymbol}/navigate/{navWaypoint}",
            async (string shipSymbol, string navWaypoint, [FromServices] ShipService shipService, [FromServices] IMapper mapper) => {
                var result = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
                if (result is null) {
                    return Results.NoContent();
                }

                return Results.Ok(result);
            });
        return group;
    }
    
    public static RouteGroupBuilder MapLocationAPI(this RouteGroupBuilder group) {
        group.MapGet("/{systemSymbol}", async (string systemSymbol, [FromServices] LocationService locationService, [FromServices] IMapper mapper) => {
            var result = await locationService.GetSystemAsync(systemSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = mapper.Map<SystemDTO>(result);
            return Results.Ok(dto);
        });
        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService, [FromServices] IMapper mapper) => {
            var result = await locationService.GetWaypointAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = mapper.Map<WaypointDTO>(result);
            return Results.Ok(dto);
        });
        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/market", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService, [FromServices] IMapper mapper) => {
            var result = await locationService.GetMarketAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = mapper.Map<MarketDTO>(result);
            return Results.Ok(dto);
        });
        return group;
    }
}