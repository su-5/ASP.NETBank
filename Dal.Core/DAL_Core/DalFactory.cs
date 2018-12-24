using Dal.Core.GenericRepositoryModel.Interfaces;
using Dal.Core.GenericRepositoryModel.Repository;

namespace Dal.Core.DAL_Core
{
    public class DalFactory : IDalFantory
    {
        private IUserDal _user;
        private AspNetBankEntities _dbContext;
        private readonly IDbFactory _dbFactory;

        public DalFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public AspNetBankEntities DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());
        public IUserDal UserDal => _user ?? (_user = new UserDal(_dbFactory));
    }
}