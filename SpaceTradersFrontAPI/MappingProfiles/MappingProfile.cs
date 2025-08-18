using AutoMapper;
using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;
// todo Do I need to map all values? Automapper seems to be smart enough to figure out some of them. See WaypointDTO.
public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Agent?, AgentDTO>()
            // map Agent symbol to AgentDTO symbol
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol));
        CreateMap<Ship?, ShipDTO>()
            .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Nav.Route.Destination))
            .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Nav.Route.Origin))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Nav.Status))
            .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
            .ForMember(dest => dest.CurrentFuel, opt => opt.MapFrom(src => src.Fuel.Current))
            .ForMember(dest => dest.FuelCapacity, opt => opt.MapFrom(src => src.Fuel.Capacity))
            // map Ship symbol to ShipDTO symbol
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
        CreateMap<Market?, MarketDTO>();
    }
}