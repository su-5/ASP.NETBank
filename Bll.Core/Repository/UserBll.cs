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
    }
}
