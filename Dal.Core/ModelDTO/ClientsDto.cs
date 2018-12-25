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
        public string DateBirth { get; set; }
        public string PassportSeries { get; set; } //Серия паспорта
        public string PassportNo { get; set; } //№ паспорта
        public string PassportIssuedBy { get; set; } //Кем выдан
        public string DateIssuePassport { get; set; } //Дата выдачи
        public string IDNumber { get; set; } // Идент. номер
        public string PhoneHouse { get; set; } //
        public string MobilePhone { get; set; } //
        public string E_mail { get; set; }
    }
}
