namespace SpaceTradersLib.Models;

public record Construction(
    string symbol,
    ConstructionMaterial[] materials,
    bool isComplete
);

public record ConstructionMaterial(
    string tradeSymbol,
    int required,
    int fulfilled
);

public record ConstructionRoot(Construction data);
