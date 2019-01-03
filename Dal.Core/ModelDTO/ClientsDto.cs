namespace Dal.Core.ModelDTO
{
    public class ClientsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } //Имя
        public string Surname { get; set; } //Фамилия
        public string MiddleName { get; set; } //Отчество
        public string DateBirth { get; set; } //Дата рождения
        public string PassportSeries { get; set; } //Серия паспорта
        public string PassportNo { get; set; } //№ паспорта
        public string PassportIssuedBy { get; set; } //Кем выдан
        public string DateIssuePassport { get; set; } //Дата выдачи
        public string IDNumber { get; set; } // Идент. номер
        public string PhoneHouse { get; set; } // тел фомашний
        public string MobilePhone { get; set; } // тел моб.
        public string E_mail { get; set; }
        public int? PlaceOfWorkID { get; set; } // мето работы Id
        public string Position { get; set; }
        public int CityRegistrationID { get; set; } //Город проживания
        public string PlaceResidence { get; set; } //Адрес прописки
        public int FamilyStatusID { get; set; }
        public int CitizenshipID { get; set; }
        public int DisabilityID { get; set; }
        public bool? Retiree { get; set; }
        public double? MonthlyIncome { get; set; }
        public bool? LiableMilitaryService { get; set; }
        public string PlaceBirth { get; set; } //Место рождения
        public int CityFactResidenceID { get; set; }
        public string ActualAddress { get; set; } // Адрес факт.проживания
        public int SexID { get; set; } // пол ID
        public PlaceOfWork PlaceOfWork { get; set; }
        public Citizenship Citizenship { get; set; }
        public City City { get; set; }
        public City City1 { get; set; }
        public DisabilityDto Disability { get; set; }
        public FamilyStatus FamilyStatus { get; set; }
        public Sex Sex { get; set; } // пол
    }
}
