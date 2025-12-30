using SpaceTradersLib.Models;

namespace SpaceTradersFrontAPI.Models;

public class MarketDto
{
    public required string Symbol { get; set; }
    public required Exports[] Exports { get; set; }
    public required Imports[] Imports { get; set; }
    public required Exchange[] Exchange { get; set; }
    public required Transactions[] Transactions { get; set; }
    public required TradeGoods[] TradeGoods { get; set; }
}