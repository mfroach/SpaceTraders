using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Models;

public class WaypointDto
{
    public string Symbol { get; set; }
    public string Type { get; set; }
    public string SystemSymbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Orbital[] Orbitals { get; set; }
    public Trait[] Traits { get; set; }
    public bool IsUnderConstruction { get; set; }
    public Faction Faction { get; set; }
    public object[] Modifiers { get; set; }
    public Chart Chart { get; set; }
}