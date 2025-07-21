using System;
using System.Text.Json.Serialization;

namespace SpaceTraders.Models;

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