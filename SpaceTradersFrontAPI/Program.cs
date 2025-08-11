using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpaceTradersFrontAPI.Database;
using SpaceTradersFrontAPI.DataTransfer;
using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Http;

namespace SpaceTradersFrontAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAutoMapper(cfg => {}, typeof(Program).Assembly);
        builder.Services.AddScoped<BaseApiService, AgentService>(); // I do not know wtf this is actually doing. I think I am not implementing correctly.
        // how the f do we initalize httpclient and pass it to services?
        var app = builder.Build();

        app.MapGroup("/my").MapMyAPI();
        //app.MapGroup("/ships").MapShipsAPI();
        //app.MapGroup("/systems").MapSystemsAPI();

        app.Run();
    }
}

public static class RouteGroupExtensions {
    public static RouteGroupBuilder MapMyAPI(this RouteGroupBuilder group) {
        group.MapGet("/agent", async (AgentService agentService, IMapper mapper) => {
            var agent = await agentService.GetAgentAsync(); // GetAgentAsync is not going to work until we initialize httpclient and somehow pass in
            if (agent is null) { return Results.NoContent(); }
            var dto = mapper.Map<AgentDTO>(agent);
            return Results.Ok(dto);
        });
        return group;
    }
}