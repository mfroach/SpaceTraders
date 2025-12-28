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
        var app = builder.Build();

        app.MapGroup("/my").MapMyAPI();
        app.MapGroup("/ships").MapShipsAPI();
        app.MapGroup("/systems").MapLocationAPI();
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
            if (agent is null) { return Results.NoContent(); }
            var dto = agent.ToDto();
            return Results.Ok(dto);
        });
        return group;
    }

    public static RouteGroupBuilder MapShipsAPI(this RouteGroupBuilder group) {
        // get ship info by symbol
        group.MapGet("/{shipSymbol}",
            async (string shipSymbol, [FromServices] ShipService shipService) => {
                var ship = await shipService.GetShipAsync(shipSymbol);
                if (ship is null) {
                    return Results.NoContent();
                }
                var dto = ship.ToDto();
                return Results.Ok(dto);
            });

        group.MapGet("/{shipSymbol}/{oneShot}",
            async (string shipSymbol, string oneShot, [FromServices] ShipService shipService) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, oneShot);
                if (result is null) {
                    return Results.NoContent();
                }
                return Results.Ok(result);
            }); // known endpoints: orbit dock warp scrap repair siphon survey scan/systems scan/waypoints extract negotiate/contract chart

        group.MapGet("/{shipSymbol}/orbit",
            async (string shipSymbol, [FromServices] ShipService shipService) => {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, "orbit");
                if (result is null) {
                    return Results.NoContent();
                }
                return Results.Ok(result);
            });

        group.MapGet("/{shipSymbol}/navigate/{navWaypoint}",
            async (string shipSymbol, string navWaypoint, [FromServices] ShipService shipService) => {
                var result = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
                if (result is null) {
                    return Results.NoContent();
                }

                return Results.Ok(result);
            });

        return group;
    }
    
    public static RouteGroupBuilder MapLocationAPI(this RouteGroupBuilder group) {
        group.MapGet("/{systemSymbol}", async (string systemSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetSystemAsync(systemSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetWaypointAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });
        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/market", async (string systemSymbol, string waypointSymbol, [FromServices] LocationService locationService) => {
            var result = await locationService.GetMarketAsync(systemSymbol + "-" + waypointSymbol);
            if (result is null) {
                return Results.NoContent();
            }

            var dto = result.ToDto();
            return Results.Ok(dto);
        });
        return group;
    }
}