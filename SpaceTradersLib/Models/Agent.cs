using System.Text.Json.Serialization;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace SpaceTradersLib.Models;

internal record AgentResponseWrapper([property: JsonPropertyName("data")] Agent Data);
internal record AgentRegisteredResponseWrapper([property: JsonPropertyName("data")] AgentRegistered Data);


public record Agent(
    [property: JsonPropertyName("accountId")]
    string AccountId,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("headquarters")]
    string Headquarters,
    [property: JsonPropertyName("credits")]
    long Credits,
    [property: JsonPropertyName("startingFaction")]
    string StartingFaction,
    [property: JsonPropertyName("shipCount")]
    int ShipCount
);

public record AgentRegistered(
    string token,
    Agent agent,
    Faction faction,
    Contract contract,
    Ship[] ships
);

public record RegisterAgentData(
    string symbol,
    string faction
    );