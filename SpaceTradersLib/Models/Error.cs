// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace SpaceTradersLib.Models;

public record ErrorRoot(
    Error error
);

public record Error(
    int code,
    string message,
    object data,
    string requestId
);

