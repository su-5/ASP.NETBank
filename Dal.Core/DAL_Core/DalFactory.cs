using Dal.Core.GenericRepositoryModel.Interfaces;
using Dal.Core.GenericRepositoryModel.Repository;

namespace Dal.Core.DAL_Core
{
    public class DalFactory : IDalFantory
    {
        private IUserDal _user;
        private AspNetBankEntities _dbContext;
        private readonly IDbFactory _dbFactory;
        private ICityDal _cityDal;
        private ICitizenshipDal _citizenshipDal;
        private IPlaceOfWorkDal _placeOfWorkDal;
        private IDisabilityDal _disabilityDal;

        public DalFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public AspNetBankEntities DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());
        public IUserDal UserDal => _user ?? (_user = new UserDal(_dbFactory));
        public ICityDal CityDal=> _cityDal ?? (_cityDal = new CityDal(_dbFactory));
        public ICitizenshipDal CitizenshipDal => _citizenshipDal ?? (_citizenshipDal = new CitizenshipDal(_dbFactory));
        public IPlaceOfWorkDal PlaceOfWorkDal => _placeOfWorkDal ?? (_placeOfWorkDal = new PlaceOfWorkDal(_dbFactory));
        public IDisabilityDal DisabilityDal => _disabilityDal ?? (_disabilityDal = new DisabilityDal(_dbFactory));
    }
}