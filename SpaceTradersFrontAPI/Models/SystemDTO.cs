using SpaceTradersLib.Models;
namespace SpaceTradersFrontAPI.Models;

public class SystemDTO {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string SectorSymbol { get; set; }
    public string Type { get; set; }
    public string Constellation { get; set; }
    public SystemWaypoint[] Waypoints { get; set; }
    public Faction[] Factions { get; set; }
}