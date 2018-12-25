using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Core.ModelDTO
{
    public class ClientsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public System.DateTime DateBirth { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNo { get; set; }
        public string PassportIssuedBy { get; set; }
        public System.DateTime DateIssuePassport { get; set; }
        public string IDNumber { get; set; }
        public string PhoneHouse { get; set; }
        public string MobilePhone { get; set; }
        public string E_mail { get; set; }
    }
}
