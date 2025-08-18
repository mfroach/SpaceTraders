using System.Text.Json.Nodes;
using Json.Schema;
using SpaceTradersLib.Models;

namespace SpaceTradersLib.Services;

public class JsonTypeDetector {
    private readonly Dictionary<Type, JsonSchema> _schemas = new() {
        {
            typeof(Contract),
            JsonSchema.FromFile("C:/Users/MichaelRoach/RiderProjects/SpaceTraders/SpaceTradersLib/Schemas/Contract.json")
        }, {
            typeof(Agent),
            JsonSchema.FromFile("C:/Users/MichaelRoach/RiderProjects/SpaceTraders/SpaceTradersLib/Schemas/Agent.json")
        }
    };
    
    // todo this is not going to work AFAICT until we unwrap payload from 'data' wrapper
    // also most methods return Stream, not string. dunno about that
    public Type? GetTypeForJson(string jsonString) { 
        var jsonNode = JsonNode.Parse(jsonString);    
        if (jsonNode == null) { return null; }
        foreach (var (type, schema) in _schemas) {
            var validationResult = schema.Evaluate(jsonNode);
            if (validationResult.IsValid) { return type; }
        }  return null;
    }
}