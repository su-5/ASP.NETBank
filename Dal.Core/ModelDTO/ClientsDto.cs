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
        public Nullable<int> PlaceOfWorkID { get; set; }
        public string Position { get; set; }
        public int CityRegistrationID { get; set; }
        public string PlaceResidence { get; set; }
        public int FamilyStatusID { get; set; }
        public int CitizenshipID { get; set; }
        public int DisabilityID { get; set; }
        public bool Retiree { get; set; }
        public double? MonthlyIncome { get; set; }
        public bool LiableMilitaryService { get; set; }
        public string PlaceBirth { get; set; }
        public int CityFactResidenceID { get; set; }
        public string ActualAddress { get; set; }
        public int SexID { get; set; }
        public virtual PlaceOfWork PlaceOfWork { get; set; }
        public virtual Citizenship Citizenship { get; set; }
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
        public virtual Disability Disability { get; set; }
        public virtual FamilyStatus FamilyStatus { get; set; }
        public virtual Sex Sex { get; set; }
    }
}
