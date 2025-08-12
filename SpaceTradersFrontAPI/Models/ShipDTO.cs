namespace SpaceTradersFrontAPI.Models;

public class ShipDTO {
    public string Symbol { get; set; }
    public string Destination { get; set; }
    public string Origin { get; set; }
    public string Status { get; set; }
    public string Cargo { get; set; }
    public string CurrentFuel { get; set; }
    public string FuelCapacity { get; set; }
}