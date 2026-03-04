namespace SpaceTradersLib.Models;

public record FactionListRoot(FactionDetails[] data, Meta meta);
public record FactionDetailsRoot(FactionDetails data);

public record FactionDetails(
    string symbol,
    string name,
    string description,
    string headquarters,
    FactionTrait[] traits,
    bool isRecruiting
);

public record FactionTrait(
    string symbol,
    string name,
    string description
);
