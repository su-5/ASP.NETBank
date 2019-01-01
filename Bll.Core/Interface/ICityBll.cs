using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Core.ModelDTO;

namespace Bll.Core.Interface
{
    public interface ICityBll
    {
        List<CityDto> GetAllCity();
    }
}
