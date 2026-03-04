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
            Registration = ship.Registration,
            Nav = ship.Nav,
            Crew = ship.Crew,
            Frame = ship.Frame,
            Reactor = ship.Reactor,
            Engine = ship.Engine,
            Modules = ship.Modules,
            Mounts = ship.Mounts,
            Cargo = ship.Cargo,
            Fuel = ship.Fuel,
            Cooldown = ship.Cooldown
        };
    }
}