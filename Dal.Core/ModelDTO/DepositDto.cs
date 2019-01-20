using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Core.ModelDTO
{
    public class DepositDto
    {
        public int DepositId { get; set; }
        public int ClientId { get; set; }
        public int CurrensyId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public double Amount { get; set; }
        public int InterestDeposit { get; set; }
        public string Contract { get; set; }
        public int AccountId { get; set; }
    }
}
