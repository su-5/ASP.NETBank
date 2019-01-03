using System.Collections.Generic;
using Dal.Core.ModelDTO;

namespace Bll.Core.Interface
{
    public interface IUserBll
    {
        List<ClientsDto> GetAll();
        List<CitizenshipDto> GetAllCitizenship();
        List<PlaceOfWorkDto> GetAllPlaceOfWork();
        List<DisabilityDto> GetAllDisability();
        void AddClientDataBase(ClientsDto client);
        void EditClientDataBase(ClientsDto client);
        void DeleteClientDataBase(int id);
    }
}