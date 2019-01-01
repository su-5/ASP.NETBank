using Dal.Core.DAL_Core;
using Dal.Core.GenericRepository;
using Dal.Core.GenericRepositoryModel.Interfaces;

namespace Dal.Core.GenericRepositoryModel.Repository
{
    public class DisabilityDal : GenericRepository<Disability>, IDisabilityDal
    {
        public DisabilityDal(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
