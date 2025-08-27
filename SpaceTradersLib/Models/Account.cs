using System;
using System.Text.Json.Serialization;
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace SpaceTradersLib.Models;

internal record AccountResponseWrapper(
    [property: JsonPropertyName("data")] AccountDataPayload Data
);

internal record AccountDataPayload(
    [property: JsonPropertyName("account")]
    Account AccountDetails
);

public record Account(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt
);