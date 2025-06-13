using System.Text.Json.Serialization;

namespace SpaceTraders.Models;

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