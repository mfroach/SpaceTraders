namespace SpaceTradersFrontAPI.Models;

public class MarketDTO
{
    public required string symbol { get; set; }
    public required Exports[] exports { get; set; }
    public required Imports[] imports { get; set; }
    public required Exchange[] exchange { get; set; }
    public required Transactions[] transactions { get; set; }
    public required TradeGoods[] tradeGoods { get; set; }
}

public class Exports
{
    public string? symbol { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
}

public class Imports
{
    public string? symbol { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
}

public class Exchange
{
    public string? symbol { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
}

public class Transactions
{
    public string? waypointSymbol { get; set; }
    public string? shipSymbol { get; set; }
    public string? tradeSymbol { get; set; }
    public string? type { get; set; }
    public int? units { get; set; }
    public int? pricePerUnit { get; set; }
    public int? totalPrice { get; set; }
    public string? timestamp { get; set; }
}

public class TradeGoods
{
    public string? symbol { get; set; }
    public string? type { get; set; }
    public int? tradeVolume { get; set; }
    public string? supply { get; set; }
    public string? activity { get; set; }
    public int? purchasePrice { get; set; }
    public int? sellPrice { get; set; }
}

