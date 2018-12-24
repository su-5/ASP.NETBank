using Dal.Core.GenericRepositoryModel.Interfaces;

namespace Dal.Core.DAL_Core
{
    public interface IDalFantory
    {
        IUserDal UserDal { get; }
    }
}