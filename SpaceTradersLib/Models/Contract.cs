namespace SpaceTradersLib.Models;

public record ContractListRoot(Contract[] data);
public record ContractRoot(Contract data);
public record ContractAcceptRoot(ContractAcceptData data);
public record ContractAcceptData(Contract contract, Agent agent);
public record Contract(
    string id,
    string factionSymbol,
    string type,
    Terms terms,
    bool accepted,
    bool fulfilled,
    string deadlineToAccept
);

public record Terms(
    string deadline,
    Payment payment,
    Deliver[] deliver
);

public record Payment(
    int onAccepted,
    int onFulfilled
);

public record Deliver(
    string tradeSymbol,
    string destinationSymbol,
    int unitsRequired,
    int unitsFulfilled
);