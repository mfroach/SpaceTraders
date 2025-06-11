using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceTraders.Models;

public record Orbital(
    [property: JsonPropertyName("symbol")] string Symbol
);