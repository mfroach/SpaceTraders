using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Mapping;

public static class SystemExtensions
{
    public static SystemDto ToDto(this SystemDetails system)
    {
        return new SystemDto
        {
            Symbol = system.Symbol,
            Name = system.Name,
            SectorSymbol = system.SectorSymbol,
            Type = system.Type,
            Constellation = system.Constellation,
            Waypoints = system.Waypoints,
            Factions = system.Factions
        };
    }

    public static WaypointDto ToDto(this Waypoint waypoint)
    {
        return new WaypointDto
        {
            Symbol = waypoint.Symbol,
            Type = waypoint.Type,
            SystemSymbol = waypoint.SystemSymbol,
            X = waypoint.X,
            Y = waypoint.Y,
            Orbitals = waypoint.Orbitals,
            Traits = waypoint.Traits,
            IsUnderConstruction = waypoint.IsUnderConstruction,
            Faction = waypoint.Faction,
            Modifiers = waypoint.Modifiers,
            Chart = waypoint.Chart,
        };
    }

    public static MarketDto ToDto(this Market market)
    {
        return new MarketDto
        {
            Symbol = market.Symbol,
            Exports = market.Exports,
            Imports = market.Imports,
            Exchange = market.Exchange,
            Transactions = market.Transactions,
            TradeGoods = market.TradeGoods,
        };
    }
}