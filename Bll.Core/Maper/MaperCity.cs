using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Maper
{
    public class MaperCity : Profile
    {
        public MaperCity()
        {
            CreateMap<City, CityDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
