﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Core.DAL_Core
{
    public interface IDbFactory
    {
        AspNetBankEntities Init();
    }
}
