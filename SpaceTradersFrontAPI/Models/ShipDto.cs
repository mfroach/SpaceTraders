using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Models;

public class ShipDto {
    public string Symbol { get; set; }
    public Registration Registration { get; set; }
    public Nav Nav { get; set; }
    public Crew Crew { get; set; }
    public Frame Frame { get; set; }
    public Reactor Reactor { get; set; }
    public Engine Engine { get; set; }
    public Module[] Modules { get; set; }
    public Mount[] Mounts { get; set; }
    public SpaceTradersLib.Models.Cargo Cargo { get; set; }
    public Fuel Fuel { get; set; }
    public Cooldown Cooldown { get; set; }
}