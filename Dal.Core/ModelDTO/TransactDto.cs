using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Core.ModelDTO
{
    public class TransactDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public int ChartAccountsId { get; set; }
        public decimal Dedet { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public int DepositId { get; set; }
        public string Contract { get; set; }
        public int UserId { get; set; }
        public int CurrensyId { get; set; }
        public string DateBegin { get; set; }
        public System.DateTime DateEnd { get; set; }
        public System.DateTime DateCreat { get; set; }
        public int InterestDeposit { get; set; }
        public DateTime? dateUpdate { get; set; }
    }
}
