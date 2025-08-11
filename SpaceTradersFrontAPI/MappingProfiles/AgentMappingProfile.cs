using AutoMapper;
using SpaceTradersFrontAPI.Models;
using SpaceTradersLib.Models;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Agent?, AgentDTO>()
            // Just an example, mapping one property to another
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol));
    }
}