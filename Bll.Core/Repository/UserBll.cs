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
    public class UserBll : IUserBll
    {
        private readonly DalFactory _dalFactory;
        // AspNetBankEntities db = new AspNetBankEntities();
        private int СontrolKey { get; set; } = 5;

        public UserBll(DalFactory dalFactory)
        {
            _dalFactory = dalFactory;
        }

        public List<ClientsDto> GetAll()
        {
            var result = Mapper.Map<List<User>, List<ClientsDto>>(_dalFactory.UserDal.GetAll().Where(w => w.DateDelete == null).ToList());
            return result.OrderBy(r => r.Surname).ToList();
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
            _dalFactory.UserDal.UpdateVoid(dbObj, dbObj.Id);
        }

        public void DeleteClientDataBase(int id)
        {
            var entity = _dalFactory.UserDal.GetById(id);
            entity.DateDelete = DateTime.Now;
            _dalFactory.UserDal.UpdateVoid(entity, entity.Id);
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> allList = _dalFactory.DbContext.User.OrderBy(e => e.MiddleName).ToList();
            var result = Mapper.Map<List<User>, List<UserDto>>(allList);
            return result;
        }


        /// <summary>
        /// При заключении договора должны создаваться как минимум два счета (для основной суммы и обслуживания процентов по депозиту) в соответствии с планом счетов:
        /// номер счета(13-значный, правила ниже);
        /// код счета из плана счетов(по нормам бух.учета банков РБ);
        /// активность счета из плана счетов(актив., пассив., актив.-пассив.);
        /// дебет, кредит, сальдо(остаток); 
        /// название счета(ФИО клиента) и др.необходимые поля.
        /// </summary>
        /// <param name="deposit"></param>
        public void AddDeposit(DepositDto deposit)
        {
            // начало транзакции
            using (var trans = _dalFactory.DbContext.Database.BeginTransaction())
            {
                try
                {
                    // огганизуем цикл для создания 2- х счетов
                    for (int i = 0; i < 2; i++)
                    {
                        var chartAccountsId = 0;
                        var account = AccountСreation(ref chartAccountsId, deposit);
                        var transaction = new Transaction
                        {
                            Account = account,
                            CurrensyId = deposit.CurrensyId,
                            UserId = deposit.ClientId,
                            ChartAccountsId = chartAccountsId,
                            Balance =  (decimal) deposit.Amount,
                            Contract = deposit.Contract,
                            Credit = 0,
                            DateBegin = deposit.DateBegin,
                            DateCreat = DateTime.Now,
                            DepositId = deposit.DepositId,
                            DateEnd = deposit.DateEnd,
                            Dedet = (decimal) deposit.Amount,
                            InterestDeposit = deposit.InterestDeposit                          
                        };

                        _dalFactory.DbContext.Transaction.Add(transaction);
                       
                    }
                    _dalFactory.DbContext.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }

        }

        ///  <summary>
        ///  Создание номера банковского счета
        /// Номер счета: 13 цифр 
        /// 1-4 – это номер балансового счета, который регламентируется планом счетов бухгалтерского учета в банках.
        ///  5-12 – это номер индивидуального счета, порядок нумерации которого может определяться банками самостоятельно.Например, клиентский счет состоит из банковской кодировки клиента (первые 5 цифр из 8) и порядкового номера одного из счетов конкретного клиента(следующие 3 цифры). 
        /// 13 - контрольный ключ
        ///  </summary>
        /// <param name="chartAccountsId"></param>
        /// <param name="deposit"></param>
        ///  <returns></returns>
        private string AccountСreation(ref int chartAccountsId ,DepositDto deposit)
        {
            string account = String.Empty;
            // находим счет для открытия депозита
            // 1521 Срочные вклады (депозиты), размещенные в банках-резидентах
            if (deposit.DepositId == 1)
            {
                var сhartAccounts = _dalFactory.DbContext.ChartAccounts.FirstOrDefault(f => f.AccountNumber == "1521");
                if (сhartAccounts != null)
                {
                    chartAccountsId = сhartAccounts.Id;
                    account = сhartAccounts.AccountNumber.Trim();
                }
            }
                     
            Random rand = new Random();
            while (account.Length < 12)
            {
                var temp = rand.Next(10);
                account += temp;
            }
            account += СontrolKey;
            return account;

        }
    }
}
