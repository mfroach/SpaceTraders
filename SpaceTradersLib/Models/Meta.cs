using System.Text.Json.Serialization;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace SpaceTradersLib.Models;

public record Meta(
    [property: JsonPropertyName("total")] int Total,
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("limit")] int Limit
);