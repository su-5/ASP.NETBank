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
    public class MaperClientsUsers : Profile
    {
        public MaperClientsUsers()
        {
            CreateMap<User, ClientsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.DateBirth, opt => opt.MapFrom(src => src.DateBirth.ToString("dd/MM/yyyy")))
                .ForMember(d => d.DateIssuePassport, opt => opt.MapFrom(src => src.DateIssuePassport.ToString("dd/MM/yyyy")));

            //    CreateMap<CountryDTO, Country>() DateIssuePassport
            //        .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
