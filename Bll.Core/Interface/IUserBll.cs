using System.Collections.Generic;
using Dal.Core.ModelDTO;

namespace Bll.Core.Interface
{
    public interface IUserBll
    {
        List<ClientsDto> GetAll();
    }
}