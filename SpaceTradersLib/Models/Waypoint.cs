using System.Text.Json.Serialization;

namespace SpaceTradersLib.Models;

internal record WaypointResponseWrapper([property: JsonPropertyName("data")] Waypoint Data);
internal record WaypointListResponseWrapper([property: JsonPropertyName("data")] Waypoint[] Data);

public record Waypoint(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("systemSymbol")]
    string SystemSymbol,
    [property: JsonPropertyName("x")] int X,
    [property: JsonPropertyName("y")] int Y,
    [property: JsonPropertyName("orbitals")]
    Orbital[] Orbitals,
    [property: JsonPropertyName("traits")] Trait[] Traits,
    [property: JsonPropertyName("isUnderConstruction")]
    bool IsUnderConstruction,
    [property: JsonPropertyName("faction")]
    Faction Faction,
    [property: JsonPropertyName("modifiers")]
    object[] Modifiers,
    [property: JsonPropertyName("chart")] Chart Chart
);

public record Trait(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description
);

public record Faction(
    [property: JsonPropertyName("symbol")] string Symbol
);

public record Chart(
    [property: JsonPropertyName("waypointSymbol")]
    string WaypointSymbol,
    [property: JsonPropertyName("submittedBy")]
    string SubmittedBy,
    [property: JsonPropertyName("submittedOn")]
    DateTime SubmittedOn
);

public record Orbital(
    [property: JsonPropertyName("symbol")] string Symbol
);