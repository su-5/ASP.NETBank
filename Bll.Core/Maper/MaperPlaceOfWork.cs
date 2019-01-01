using AutoMapper;
using Dal.Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Maper
{
    public class MaperPlaceOfWork : Profile
    {
        public MaperPlaceOfWork()
        {
            CreateMap<PlaceOfWork, PlaceOfWorkDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
