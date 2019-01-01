using AutoMapper;
using Bll.Core.Maper;

namespace Bll.Core
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MaperClientsUsers>();
                x.AddProfile<MaperCity>();
                x.AddProfile<MaperPlaceOfWork>();
                x.AddProfile<MaperCitizenship>();
                x.AddProfile<MaperDisability>();
            });


        }
    }
}
