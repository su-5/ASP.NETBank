using AutoMapper;
using Dal.Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Maper
{
    public class MaperCitizenship : Profile
    {
        public MaperCitizenship()
        {
            CreateMap<Citizenship, CitizenshipDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
