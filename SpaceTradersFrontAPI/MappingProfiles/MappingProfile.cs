using AutoMapper;
using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;

// todo can we generate the mapping?
public class AppMappingProfile : Profile {
    public AppMappingProfile()
    {
        CreateMap<Agent?, AgentDTO>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol));
        CreateMap<Ship?, ShipDTO>()
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Nav.Route.Destination))
            .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Nav.Route.Origin))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Nav.Status))
            .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
            .ForMember(dest => dest.CurrentFuel, opt => opt.MapFrom(src => src.Fuel.Current))
            .ForMember(dest => dest.FuelCapacity, opt => opt.MapFrom(src => src.Fuel.Capacity))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol));
        CreateMap<SystemDetails?, SystemDTO>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Constellation, opt => opt.MapFrom(src => src.Constellation))
            .ForMember(dest => dest.SectorSymbol, opt => opt.MapFrom(src => src.SectorSymbol))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Factions, opt => opt.MapFrom(src => src.Factions))
            .ForMember(dest => dest.Waypoints, opt => opt.MapFrom(src => src.Waypoints));
        CreateMap<Waypoint?, WaypointDTO>()
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.orbitals, opt => opt.MapFrom(src => src.Orbitals));
        CreateMap<Orbital?, Orbitals>()
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol));
        CreateMap<Market?, MarketDTO>()
            .ForMember(dest => dest.imports, opt => opt.MapFrom(src => src.imports));
        CreateMap<SpaceTradersLib.Models.Imports, SpaceTradersFrontAPI.Models.Imports>()
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.symbol))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description));
        CreateMap<SpaceTradersLib.Models.Exchange, SpaceTradersFrontAPI.Models.Exchange>()
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.symbol))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description));
        CreateMap<SpaceTradersLib.Models.Transactions, SpaceTradersFrontAPI.Models.Transactions>()
            .ForMember(dest => dest.waypointSymbol, opt => opt.MapFrom(src => src.waypointSymbol))
            .ForMember(dest => dest.shipSymbol, opt => opt.MapFrom(src => src.shipSymbol))
            .ForMember(dest => dest.tradeSymbol, opt => opt.MapFrom(src => src.tradeSymbol))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.units, opt => opt.MapFrom(src => src.units))
            .ForMember(dest => dest.pricePerUnit, opt => opt.MapFrom(src => src.pricePerUnit))
            .ForMember(dest => dest.totalPrice, opt => opt.MapFrom(src => src.totalPrice))
            .ForMember(dest => dest.timestamp, opt => opt.MapFrom(src => src.timestamp));
        CreateMap<SpaceTradersLib.Models.TradeGoods, SpaceTradersFrontAPI.Models.TradeGoods>()
            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.symbol))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.tradeVolume, opt => opt.MapFrom(src => src.tradeVolume))
            .ForMember(dest => dest.supply, opt => opt.MapFrom(src => src.supply))
            .ForMember(dest => dest.activity, opt => opt.MapFrom(src => src.activity))
            .ForMember(dest => dest.purchasePrice, opt => opt.MapFrom(src => src.purchasePrice))
            .ForMember(dest => dest.sellPrice, opt => opt.MapFrom(src => src.sellPrice));
    }
}