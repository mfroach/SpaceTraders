namespace SpaceTradersLib.Models;

public record JumpGate(
    string symbol,
    ConnectedSystem[] connections
);

public record ConnectedSystem(
    string symbol,
    string sectorSymbol,
    string type,
    string factionSymbol,
    int x,
    int y,
    int distance
);

public record JumpGateRoot(JumpGate data);
