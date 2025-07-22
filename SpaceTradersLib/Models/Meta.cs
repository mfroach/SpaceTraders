using System.Text.Json.Serialization;

namespace SpaceTradersLib.Models;

public record Meta(
    [property: JsonPropertyName("total")] int Total,
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("limit")] int Limit
);