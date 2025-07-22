using System.Text.Json.Serialization;
using SpaceTradersLib.Models;

namespace SpaceTradersLib.Models;

// Serialization records

public record NavData(string waypointSymbol);

// Deserialization records

internal record ShipListResponseWrapper(
    [property: JsonPropertyName("data")] Ship[] Data,
    [property: JsonPropertyName("meta")] Meta Meta
    );

internal record ShipStatusResponseWrapper(
    [property: JsonPropertyName("data")] Ship Data
);

public record ShipResponseWrapper(
    [property: JsonPropertyName("data")] Ship Data
);

public record Registration(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("factionSymbol")]
    string FactionSymbol,
    [property: JsonPropertyName("role")] string Role
);

public record RouteWaypoint(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("systemSymbol")]
    string SystemSymbol,
    [property: JsonPropertyName("x")] int X,
    [property: JsonPropertyName("y")] int Y
);

public record Route(
    [property: JsonPropertyName("destination")]
    RouteWaypoint Destination,
    [property: JsonPropertyName("origin")] RouteWaypoint Origin,
    [property: JsonPropertyName("departureTime")]
    DateTime DepartureTime,
    [property: JsonPropertyName("arrival")]
    DateTime Arrival
);

public record Nav(
    [property: JsonPropertyName("systemSymbol")]
    string SystemSymbol,
    [property: JsonPropertyName("waypointSymbol")]
    string WaypointSymbol,
    [property: JsonPropertyName("route")] Route Route,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("flightMode")]
    string FlightMode
);

public record Crew(
    [property: JsonPropertyName("current")]
    int Current,
    [property: JsonPropertyName("required")]
    int Required,
    [property: JsonPropertyName("capacity")]
    int Capacity,
    [property: JsonPropertyName("rotation")]
    string Rotation,
    [property: JsonPropertyName("morale")] int Morale,
    [property: JsonPropertyName("wages")] int Wages
);

public record Requirements(
    [property: JsonPropertyName("power")] int? Power,
    [property: JsonPropertyName("crew")] int? Crew,
    [property: JsonPropertyName("slots")] int? Slots
);

public record Frame(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description,
    [property: JsonPropertyName("condition")]
    double Condition,
    [property: JsonPropertyName("integrity")]
    double Integrity,
    [property: JsonPropertyName("moduleSlots")]
    int ModuleSlots,
    [property: JsonPropertyName("mountingPoints")]
    int MountingPoints,
    [property: JsonPropertyName("fuelCapacity")]
    int FuelCapacity,
    [property: JsonPropertyName("requirements")]
    Requirements Requirements,
    [property: JsonPropertyName("quality")]
    int Quality
);

public record Reactor(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description,
    [property: JsonPropertyName("condition")]
    double Condition,
    [property: JsonPropertyName("integrity")]
    double Integrity,
    [property: JsonPropertyName("powerOutput")]
    int PowerOutput,
    [property: JsonPropertyName("requirements")]
    Requirements Requirements,
    [property: JsonPropertyName("quality")]
    int Quality
);

public record Engine(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description,
    [property: JsonPropertyName("condition")]
    double Condition,
    [property: JsonPropertyName("integrity")]
    double Integrity,
    [property: JsonPropertyName("speed")] int Speed,
    [property: JsonPropertyName("requirements")]
    Requirements Requirements,
    [property: JsonPropertyName("quality")]
    int Quality
);

public record ShipModule(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description,
    [property: JsonPropertyName("requirements")]
    Requirements Requirements,
    [property: JsonPropertyName("capacity")]
    int? Capacity // optional
);

public record ShipMount(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")]
    string Description,
    [property: JsonPropertyName("requirements")]
    Requirements Requirements,
    [property: JsonPropertyName("strength")]
    int Strength,
    [property: JsonPropertyName("deposits")]
    string[]? Deposits
);

public record Cargo(
    [property: JsonPropertyName("capacity")]
    int Capacity,
    [property: JsonPropertyName("units")] int Units,
    [property: JsonPropertyName("inventory")]
    object[] Inventory
);

public record Consumed(
    [property: JsonPropertyName("amount")] int Amount,
    [property: JsonPropertyName("timestamp")]
    DateTime Timestamp
);

public record Fuel(
    [property: JsonPropertyName("current")]
    int Current,
    [property: JsonPropertyName("capacity")]
    int Capacity,
    [property: JsonPropertyName("consumed")]
    Consumed Consumed
);

public record Cooldown(
    [property: JsonPropertyName("shipSymbol")]
    string ShipSymbol,
    [property: JsonPropertyName("totalSeconds")]
    int TotalSeconds,
    [property: JsonPropertyName("remainingSeconds")]
    int RemainingSeconds
);

public record Ship(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("registration")]
    Registration Registration,
    [property: JsonPropertyName("nav")] Nav Nav,
    [property: JsonPropertyName("crew")] Crew Crew,
    [property: JsonPropertyName("frame")] Frame Frame,
    [property: JsonPropertyName("reactor")]
    Reactor Reactor,
    [property: JsonPropertyName("engine")] Engine Engine,
    [property: JsonPropertyName("modules")]
    ShipModule[] Modules,
    [property: JsonPropertyName("mounts")] ShipMount[] Mounts,
    [property: JsonPropertyName("cargo")] Cargo Cargo,
    [property: JsonPropertyName("fuel")] Fuel Fuel,
    [property: JsonPropertyName("cooldown")]
    Cooldown Cooldown
);