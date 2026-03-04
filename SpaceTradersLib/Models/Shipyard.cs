namespace SpaceTradersLib.Models;

public record Shipyard(
    string symbol,
    ShipType[] shipTypes,
    ShipyardTransaction[]? transactions,
    ShipyardShip[]? ships,
    int? modificationsFee
);

public record ShipType(
    string type
);

public record ShipyardShip(
    string type,
    string name,
    string description,
    string supply,
    string? activity,
    int purchasePrice,
    Frame frame,
    Reactor reactor,
    Engine engine,
    Module[] modules,
    Mount[] mounts,
    Crew crew
);

public record ShipyardTransaction(
    string waypointSymbol,
    string shipSymbol,
    string shipType,
    int price,
    string agentSymbol,
    string timestamp
);

public record ShipyardRoot(Shipyard data);
