using Microsoft.EntityFrameworkCore;

namespace SpaceTradersFrontAPI.Database;

internal class TradeDb : DbContext {
    public TradeDb(DbContextOptions<TradeDb> options)
        : base(options) {
    }
}