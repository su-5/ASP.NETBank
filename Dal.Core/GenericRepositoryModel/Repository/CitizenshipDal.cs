using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Core.DAL_Core;
using Dal.Core.GenericRepository;
using Dal.Core.GenericRepositoryModel.Interfaces;

namespace Dal.Core.GenericRepositoryModel.Repository
{
    public class CitizenshipDal : GenericRepository<Citizenship>, ICitizenshipDal
    {
        public CitizenshipDal(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
