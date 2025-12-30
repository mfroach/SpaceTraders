using Microsoft.AspNetCore.Mvc;
using SpaceTradersFrontAPI.Mapping;
using SpaceTradersLib.Http;

namespace SpaceTradersFrontAPI;

public class Program
{
    public static void Main(string[] args)
    {
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
        builder.Services.ConfigureHttpClientDefaults(httpClientBuilder =>
        {
            httpClientBuilder.ConfigureHttpClient(HttpClientConfigurator.ConfigureDefaultClient);
        });
        builder.Services.AddHttpClient<AgentService, AgentService>();
        builder.Services.AddHttpClient<LocationService, LocationService>();
        builder.Services.AddHttpClient<ShipService, ShipService>();
        var app = builder.Build();

        app.MapGroup("/my").MapMyAPI();
        app.MapGroup("/ships").MapShipsAPI();
        app.MapGroup("/systems").MapLocationAPI();
        app.MapPost("/register",
            async (string symbol, string faction, string accountToken, [FromServices] AccountService accountService) =>
            {
                var result = await accountService.RegisterAgent(symbol, faction, accountToken);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.InternalServerError(result.Error);
            });
        app.UseCors("AllowSvelteApp");
        app.Run();
    }
}

public static class RouteGroupExtensions
{
    public static RouteGroupBuilder MapMyAPI(this RouteGroupBuilder group)
    {
        group.MapGet("/agent", async ([FromServices] AgentService agentService) =>
        {
            var agent = await agentService.GetAgentAsync();
            if (!agent.IsSuccess || agent.Data is null)
            {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = agent.Data.ToDto();
            return Results.Ok(dto);
        });
        return group;
    }

    public static RouteGroupBuilder MapShipsAPI(this RouteGroupBuilder group)
    {
        // get ship info by symbol
        group.MapGet("/{shipSymbol}",
            async (string shipSymbol, [FromServices] ShipService shipService) =>
            {
                var ship = await shipService.GetShipAsync(shipSymbol);
                if (!ship.IsSuccess || ship.Data is null)
                {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }

                var dto = ship.Data.ToDto();
                return Results.Ok(dto);
            });

        group.MapGet("/{shipSymbol}/{oneShot}",
            async (string shipSymbol, string oneShot, [FromServices] ShipService shipService) =>
            {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, oneShot);
                if (!result.IsSuccess || result.Data is null)
                {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }

                return Results.Ok(result.Data);
            }); // known endpoints: orbit dock warp scrap repair siphon survey scan/systems scan/waypoints extract negotiate/contract chart

        group.MapGet("/{shipSymbol}/orbit",
            async (string shipSymbol, [FromServices] ShipService shipService) =>
            {
                var result = await shipService.ShipPostOneShotAsync(shipSymbol, "orbit");
                if (!result.IsSuccess || result.Data is null)
                {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }

                return Results.Ok(result.Data);
            });

        group.MapGet("/{shipSymbol}/navigate/{navWaypoint}",
            async (string shipSymbol, string navWaypoint, [FromServices] ShipService shipService) =>
            {
                var result = await shipService.NavigateShipAsync(shipSymbol, navWaypoint);
                if (!result.IsSuccess || result.Data is null)
                {
                    return Results.InternalServerError("Response was bad. Token may be invalid.");
                }

                return Results.Ok(result.Data);
            });

        return group;
    }

    public static RouteGroupBuilder MapLocationAPI(this RouteGroupBuilder group)
    {
        group.MapGet("/{systemSymbol}", async (string systemSymbol, [FromServices] LocationService locationService) =>
        {
            var result = await locationService.GetSystemAsync(systemSymbol);
            if (!result.IsSuccess || result.Data is null)
            {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.Data.ToDto();
            return Results.Ok(dto);
        });

        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}", async (string systemSymbol, string waypointSymbol,
            [FromServices] LocationService locationService) =>
        {
            var result = await locationService.GetWaypointAsync(systemSymbol + "-" + waypointSymbol);
            if (!result.IsSuccess || result.Data is null)
            {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.Data.ToDto();
            return Results.Ok(dto);
        });
        group.MapGet("/{systemSymbol}/waypoints/{waypointSymbol}/market", async (string systemSymbol,
            string waypointSymbol, [FromServices] LocationService locationService) =>
        {
            var result = await locationService.GetMarketAsync(systemSymbol + "-" + waypointSymbol);
            if (!result.IsSuccess || result.Data is null)
            {
                return Results.InternalServerError("Response was bad. Token may be invalid.");
            }

            var dto = result.Data.ToDto();
            return Results.Ok(dto);
        });
        return group;
    }
}