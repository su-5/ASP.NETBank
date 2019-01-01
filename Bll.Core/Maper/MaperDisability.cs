using AutoMapper;
using Dal.Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Maper
{
    class MaperDisability : Profile
    {
        public MaperDisability()
        {

            CreateMap<Disability, DisabilityDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<DisabilityDto, Disability>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));

        }
    }
}
