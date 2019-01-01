namespace Bll.Core.Interface
{
    public interface IBllFactory
    {
        IUserBll UserBll { get; }
        ICityBll CityBll { get; }
    }
}