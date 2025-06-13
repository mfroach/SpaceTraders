using System;
using System.Text.Json.Serialization;

namespace SpaceTraders.Models;

public record Account {
    [property: JsonPropertyName("id")] public string Id { get; init; }

    [property: JsonPropertyName("email")] public string Email { get; init; }

    [property: JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    public Account() {
        Id = string.Empty;
        Email = string.Empty;
        CreatedAt = default;
    }

    public Account(string id, string email, DateTime createdAt) {
        Id = id;
        Email = email;
        CreatedAt = createdAt;
    }
}

internal record AccountDataPayload(
    [property: JsonPropertyName("account")]
    Account AccountDetails
);

internal record AccountResponseWrapper(
    [property: JsonPropertyName("data")] AccountDataPayload Data
);