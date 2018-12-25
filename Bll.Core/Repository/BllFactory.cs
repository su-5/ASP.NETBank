using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Core.Interface;
using Dal.Core.DAL_Core;
using DAL.Core.DAL_Core;

namespace Bll.Core.Repository
{
  public  class BllFactory : IBllFactory
    {
        private readonly DalFactory _dalFactory;
        private IUserBll _userBll;
        public BllFactory()
        {
            _dalFactory = new DalFactory(new DbFactory());
        }

        public IUserBll UserBll => _userBll ?? (_userBll = new UserBll(_dalFactory));
    }
}
