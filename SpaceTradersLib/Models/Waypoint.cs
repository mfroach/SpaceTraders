using System.Text.Json.Serialization;

// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

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

public record MarketResponseWrapper(
    Market data
);

public record Market(
    string Symbol,
    Exports[] Exports,
    Imports[] Imports,
    Exchange[] Exchange,
    Transactions[] Transactions,
    TradeGoods[] TradeGoods
);

public record Exports(
    string Symbol,
    string Name,
    string Description
);

public record Imports(
    string Symbol,
    string Name,
    string Description
);

public record Exchange(
    string Symbol,
    string Name,
    string Description
);

public record Transactions(
    string WaypointSymbol,
    string ShipSymbol,
    string TradeSymbol,
    string Type,
    int Units,
    int PricePerUnit,
    int TotalPrice,
    string Timestamp
);

public record TradeGoods(
    string Symbol,
    string Type,
    int TradeVolume,
    string Supply,
    string Activity,
    int PurchasePrice,
    int SellPrice
);