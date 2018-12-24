using Dal.Core.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dal.Core.DAL_Core;
using Dal.Core.GenericRepositoryModel.Interfaces;

namespace Dal.Core.GenericRepositoryModel.Repository
{
    public class UserDal : GenericRepository<User>, IUserDal
    {
        public UserDal(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
