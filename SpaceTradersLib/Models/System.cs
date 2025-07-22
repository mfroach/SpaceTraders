using System.Text.Json.Serialization;
using SpaceTradersLib.Services;
namespace SpaceTradersLib.Models;

internal record SystemResponseWrapper([property: JsonPropertyName("data")] SystemDetails Data);

public record SystemWaypoint(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("x")] int X,
    [property: JsonPropertyName("y")] int Y,
    [property: JsonPropertyName("orbitals")] Orbital[]? Orbitals,
    [property: JsonPropertyName("orbits")] string? Orbits
);

public record SystemDetails(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("sectorSymbol")] string SectorSymbol,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("x")] int X,
    [property: JsonPropertyName("y")] int Y,
    [property: JsonPropertyName("waypoints")] SystemWaypoint[]? Waypoints,
    [property: JsonPropertyName("factions")] Faction[] Factions,
    [property: JsonPropertyName("constellation")] string Constellation,
    [property: JsonPropertyName("name")] string Name
);