using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Mapping;

public static class ShipExtensions
{
    public static ShipDto ToDto(this Ship ship)
    {
        return new ShipDto
        {
            Symbol = ship.Symbol,
            Nav = ship.Nav,
            Destination = ship.Nav.Route.Destination.SystemSymbol,
            Origin = ship.Nav.Route.Origin.SystemSymbol,
            Status = ship.Nav.Status,
            Cargo = ship.Cargo.Inventory,
            CurrentFuel = ship.Fuel.Current,
            FuelCapacity = ship.Fuel.Capacity
        };
    }
}