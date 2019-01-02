using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bll.Core.Interface;
using Dal.Core;
using Dal.Core.DAL_Core;
using Dal.Core.ModelDTO;

namespace Bll.Core.Repository
{
  public  class UserBll : IUserBll
    {
        private readonly DalFactory _dalFactory;

        public UserBll(DalFactory dalFactory)
        {
            _dalFactory = dalFactory;
        }

        public List<ClientsDto> GetAll()
        {
            var result = Mapper.Map<List<User>, List<ClientsDto>>(_dalFactory.UserDal.GetAll().ToList());
            return result.OrderBy(r => r.Name).ToList();
        }

        public List<CitizenshipDto> GetAllCitizenship()
        {
            List<CitizenshipDto> result = Mapper.Map<List<Citizenship>, List<CitizenshipDto>>(_dalFactory.CitizenshipDal.GetAll().ToList());
            return result.OrderBy(r => r.Name).ToList();
        }

        public List<PlaceOfWorkDto> GetAllPlaceOfWork()
        {
            var result = Mapper.Map<List<PlaceOfWork>, List<PlaceOfWorkDto>>(_dalFactory.PlaceOfWorkDal.GetAll().ToList());
            return result.OrderBy(r => r.NamePlaseOfWork).ToList();
        }

        public List<DisabilityDto> GetAllDisability()
        {
            var result = Mapper.Map<List<Disability>, List<DisabilityDto>>(_dalFactory.DisabilityDal.GetAll().ToList());
            return result.OrderBy(r => r.Name).ToList();
        }

        public void AddClientDataBase(ClientsDto client)
        {
            var dbObj = Mapper.Map<ClientsDto, User>(client);
            _dalFactory.UserDal.Add(dbObj);

        }

        public void EditClientDataBase(ClientsDto client)
        {
            var dbObj = Mapper.Map<ClientsDto, User>(client);
            _dalFactory.UserDal.UpdateVoid(dbObj,dbObj.Id);
        }
    }
}
