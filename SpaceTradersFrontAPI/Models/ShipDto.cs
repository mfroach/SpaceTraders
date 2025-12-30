using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Models;

public class ShipDto
{
    public string Symbol { get; set; }
    public Nav Nav { get; set; }
    public string Destination { get; set; }
    public string Origin { get; set; }
    public string Status { get; set; }
    public Inventory[] Cargo { get; set; }
    public int CurrentFuel { get; set; }
    public int FuelCapacity { get; set; }
}