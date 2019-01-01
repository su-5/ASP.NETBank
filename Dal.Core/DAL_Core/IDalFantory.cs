using Dal.Core.GenericRepositoryModel.Interfaces;

namespace Dal.Core.DAL_Core
{
    public interface IDalFantory
    {
        IUserDal UserDal { get; }

        ICityDal CityDal { get; }

        ICitizenshipDal CitizenshipDal { get; }

        IPlaceOfWorkDal PlaceOfWorkDal { get; }
        IDisabilityDal DisabilityDal { get; }
    }
}