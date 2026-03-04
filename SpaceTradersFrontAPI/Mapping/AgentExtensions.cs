using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Mapping;

public static class AgentExtensions {
    public static AgentDto ToDto(this Agent agent) {
        return new AgentDto {
            Symbol = agent.Symbol,
            AccountId = agent.AccountId,
            Headquarters = agent.Headquarters,
            Credits = agent.Credits,
            StartingFaction = agent.StartingFaction,
            ShipCount = agent.ShipCount
        };
    }
}