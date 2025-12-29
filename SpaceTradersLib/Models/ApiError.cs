// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace SpaceTradersLib.Models;

public record ErrorRoot(
    ApiError ApiError
);

public record ApiError(
    int Code,
    string Message,
    object Data,
    string RequestId
);

