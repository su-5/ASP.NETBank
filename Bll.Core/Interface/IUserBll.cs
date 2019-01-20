using System.Collections.Generic;
using Dal.Core;
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
        List<UserDto> GetAllUsers();
        void AddDeposit(DepositDto deposit);
        decimal? GetSummBank();
        List<TransactDto> GetDepositsUser(int id);
        byte[] ReportDeposit(DepositDto depositDto);
    }
}