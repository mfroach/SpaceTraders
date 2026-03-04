namespace SpaceTradersFrontAPI.Models;

public class AgentDto {
    public string Symbol { get; set; }
    public string AccountId { get; set; }
    public string Headquarters { get; set; }
    public long Credits { get; set; }
    public string StartingFaction { get; set; }
    public int ShipCount { get; set; }
}