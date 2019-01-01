using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bll.Core.Interface;
using Dal.Core;
using Dal.Core.DAL_Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Repository
{
    public class CityBll : ICityBll
    {
        private readonly DalFactory _dalFactory;
        public CityBll(DalFactory dalFactory)
        {
            _dalFactory = dalFactory;
        }
        public List<CityDto> GetAllCity()
        {
            var result = Mapper.Map<List<City>, List<CityDto>>(_dalFactory.CityDal.GetAll().ToList());
            return result.OrderBy(r => r.Name).ToList();
        }
    }
}
