using AutoMapper;
using CakeAPI.Dto;
using CakeAPI.Model;

namespace CakeAPI.Profiles
{
    public class Profiles :Profile
    {
        public Profiles()
        {
            CreateMap<CakeModel, CakeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<CakeCreateDTO,CakeModel >()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            
        }
    }
}
