using System.Text.Json.Serialization;

namespace SpaceTradersLib.Models;

internal record ContractListResponseWrapper([property: JsonPropertyName("data")] Contract[] Data);
internal record ContractResponseWrapper([property: JsonPropertyName("data")] Contract Data);

public record Contract(
    [property: JsonPropertyName("id")] string ContractID,
    [property: JsonPropertyName("factionSymbol")]
    string FactionSymbol,
    [property: JsonPropertyName("type")] string ContractType,
    [property: JsonPropertyName("terms")] Terms Terms,
    [property: JsonPropertyName("accepted")]
    bool Accepted
);

public record Terms(
    [property: JsonPropertyName("deadline")]
    DateTime Deadline,
    [property: JsonPropertyName("payment")]
    Payment Payment,
    [property: JsonPropertyName("deliver")]
    Deliver[] DeliverItems
);

public record Payment(
    [property: JsonPropertyName("onAccepted")]
    int OnAccepted,
    [property: JsonPropertyName("onFulfilled")]
    int OnFulfilled
);

public record Deliver(
    [property: JsonPropertyName("tradeSymbol")]
    string TradeSymbol,
    [property: JsonPropertyName("destinationSymbol")]
    string DestinationSymbol,
    [property: JsonPropertyName("unitsRequired")]
    int UnitsRequired,
    [property: JsonPropertyName("unitsFulfilled")]
    int UnitsFulfilled
);