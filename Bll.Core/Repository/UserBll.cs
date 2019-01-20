using AutoMapper;
using Bll.Core.Ex;
using Bll.Core.Interface;
using Dal.Core;
using Dal.Core.DAL_Core;
using Dal.Core.ModelDTO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

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
            var dublikatContract = _dalFactory.DbContext.Transactions.Any(a => a.Contract == deposit.Contract);
            if (dublikatContract)
            {
                throw new ValidationException("Данный номер доровора уже используется в системе. Задайте другой номер", "");
            }
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
                            Balance = i == 1 ? (decimal)deposit.Amount : 0,
                            Contract = deposit.Contract,
                            Credit = 0,
                            DateBegin = deposit.DateBegin,
                            DateCreat = DateTime.Now,
                            DepositId = deposit.DepositId,
                            DateEnd = deposit.DateEnd,
                            Dedet = i == 1 ? (decimal)deposit.Amount : 0,
                            InterestDeposit = deposit.InterestDeposit,
                            status = i != 1 ? "%" : null
                        };

                        _dalFactory.DbContext.Transactions.Add(transaction);

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

        public decimal? GetSummBank()
        {
            decimal? summ = 0;
            // начало транзакции

            InterestAccrual(); // расчет процентов
                               //using (var trans = _dalFactory.DbContext.Database.BeginTransaction())
                               //{
                               //    try
                               //    { // берем в расчет только базовую сумма , начисленые проценты не учитываем
            if (_dalFactory.DbContext.BankDevelopmentAccount != null)
            {
                summ = _dalFactory.DbContext.BankDevelopmentAccount.FirstOrDefault(w => w.DateDelete == null)
                    ?.Summ;
            }

            var transaction = _dalFactory.DbContext.Transactions.Where(w => w.dateUpdate == null).ToList();
            if (transaction.Any())
            {
                summ += _dalFactory.DbContext.Transactions.Where(w => w.dateUpdate == null && w.status == null).Sum(s => s.Balance);
            }

            //        trans.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        trans.Rollback();
            //    }
            //}
            return summ;
        }

        public List<TransactDto> GetDepositsUser(int id)
        {
            var transactions = _dalFactory.DbContext.Transactions.Where(w => w.UserId == id && w.dateUpdate == null).OrderBy(e => e.Account).ToList();
            var resutl = Mapper.Map<List<Transaction>, List<TransactDto>>(transactions);
            return resutl;

        }

        public byte[] ReportDeposit(DepositDto depositDto)
        {
            var dataReport = ReportGenerationData(depositDto);
            // var result = ReportGeneration(dataReport , depositDto);
            return dataReport;
        }

        private byte[] ReportGeneration(object dataReport, DepositDto depositDto)
        {
            return null;
        }

        private byte[] ReportGenerationData(DepositDto depositDto)
        {
            string path = Path.ChangeExtension(Path.GetTempFileName(), Guid.NewGuid().ToString()); // Создает на диске временный пустой файл с уникальным именем и возвращает полный путь этого файла.
            using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new HSSFWorkbook(); //Рабочая книга Excel
                ISheet sheet = workbook.CreateSheet("Депозит"); // лист exsel

                //Font heading   // создание шрифта используем 12 шрифт для заголовка
                NPOI.SS.UserModel.IFont fontHeading = workbook.CreateFont();
                fontHeading.FontHeightInPoints = 12;
                fontHeading.Boldweight = (short)FontBoldWeight.Bold;
                // имена шрифтов 10 шрифт
                NPOI.SS.UserModel.IFont fontNames = workbook.CreateFont();
                fontNames.FontHeightInPoints = 10;
                fontNames.Boldweight = (short)FontBoldWeight.Bold;

                NPOI.SS.UserModel.IFont fontStorageName = workbook.CreateFont();
                fontStorageName.FontHeightInPoints = 11;
                fontStorageName.Boldweight = (short)FontBoldWeight.Bold;

                ICellStyle borderedStyle = workbook.CreateCellStyle();
                borderedStyle.BorderBottom = BorderStyle.Thin;
                borderedStyle.BorderLeft = BorderStyle.Thin;
                borderedStyle.BorderRight = BorderStyle.Thin;
                borderedStyle.BorderTop = BorderStyle.Thin;

                ICellStyle borderedBottomStyle = workbook.CreateCellStyle();
                borderedBottomStyle.BorderBottom = BorderStyle.Thin;

                ////////heading style
                ICellStyle styleHeading = workbook.CreateCellStyle();
                styleHeading.SetFont(fontHeading); // задаем заголовку 12 шрифт
                styleHeading.Alignment = HorizontalAlignment.Center; // расположение центр
                //styleHeading.BorderBottom = BorderStyle.Thin;

                ICellStyle styleNames = workbook.CreateCellStyle();
                styleNames.CloneStyleFrom(borderedStyle);
                styleNames.SetFont(fontNames);
                styleNames.Alignment = HorizontalAlignment.Left;
                styleNames.VerticalAlignment = VerticalAlignment.Center;

                ICellStyle styleNameContractor = workbook.CreateCellStyle();
                styleNameContractor.CloneStyleFrom(borderedStyle);
                styleNameContractor.SetFont(fontNames);
                styleNameContractor.Alignment = HorizontalAlignment.Left;
                styleNameContractor.VerticalAlignment = VerticalAlignment.Center;
                styleNameContractor.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                styleNameContractor.FillPattern = FillPattern.SolidForeground;


                ICellStyle styleStorageName = workbook.CreateCellStyle();
                styleStorageName.CloneStyleFrom(borderedStyle);
                styleStorageName.SetFont(fontStorageName);
                styleStorageName.Alignment = HorizontalAlignment.Center;
                styleStorageName.VerticalAlignment = VerticalAlignment.Center;
                styleStorageName.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
                styleStorageName.FillPattern = FillPattern.SolidForeground;

                ICellStyle hederTableStyle = workbook.CreateCellStyle();
                hederTableStyle.CloneStyleFrom(borderedStyle);
                hederTableStyle.Alignment = HorizontalAlignment.Center;
                hederTableStyle.SetFont(fontNames);
                hederTableStyle.VerticalAlignment = VerticalAlignment.Center;
                hederTableStyle.WrapText = true;

                ICellStyle totalNameStyle = workbook.CreateCellStyle();
                totalNameStyle.SetFont(fontNames);
                totalNameStyle.Alignment = HorizontalAlignment.Right;
                totalNameStyle.VerticalAlignment = VerticalAlignment.Center;

                ICellStyle leftNameStyle = workbook.CreateCellStyle();
                leftNameStyle.CloneStyleFrom(totalNameStyle);
                leftNameStyle.Alignment = HorizontalAlignment.Left;

                ICellStyle rightBorderedStyle = workbook.CreateCellStyle();
                rightBorderedStyle.CloneStyleFrom(borderedStyle);
                totalNameStyle.SetFont(fontNames);
                rightBorderedStyle.Alignment = HorizontalAlignment.Right;

                //////DegitalStyle
                ICellStyle digitalStyle = workbook.CreateCellStyle();
                digitalStyle.CloneStyleFrom(borderedStyle);
                digitalStyle.VerticalAlignment = VerticalAlignment.Top;
                digitalStyle.WrapText = true;
                // digitalStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");

                //dataTableByCenter
                ICellStyle dataTableByCenter = workbook.CreateCellStyle();
                dataTableByCenter.CloneStyleFrom(digitalStyle);
                dataTableByCenter.Alignment = HorizontalAlignment.Center;
                dataTableByCenter.VerticalAlignment = VerticalAlignment.Center;

                // default settings of sheet
                sheet.DefaultRowHeight = 300;
                short rowHeightStorage = 400;
                short rowHeightCell = 300;

                //print settings
                sheet.RepeatingRows = CellRangeAddress.ValueOf("$3:$4");
                sheet.Footer.Center = "&P";
                sheet.PrintSetup.FitWidth = 1;
                sheet.PrintSetup.FitHeight = 0;
                sheet.FitToPage = true;
                sheet.PrintSetup.PaperSize = (short)PaperSize.A4_EXTRA;
                sheet.PrintSetup.Landscape = false;
                sheet.SetMargin(MarginType.TopMargin, 0.5);
                sheet.SetMargin(MarginType.BottomMargin, 0.8);
                sheet.SetMargin(MarginType.LeftMargin, 1);
                sheet.SetMargin(MarginType.RightMargin, 0.2);

                // найдем базовый депозит
                Transaction deposit = _dalFactory.DbContext.Transactions.FirstOrDefault(f => f.Id == depositDto.AccountId);
                Transaction deposit2;
                if (deposit?.status != null)
                {
                    deposit2 = deposit;
                    deposit = _dalFactory.DbContext.Transactions.Where(w => w.status == null).FirstOrDefault(f => f.Contract == deposit2.Contract);
                }
                else
                {
                    deposit2 = _dalFactory.DbContext.Transactions.Where(w => w.status != null).FirstOrDefault(f => f.Contract == deposit.Contract && f.dateUpdate == null);
                }

                List<Transaction> listOperation = _dalFactory.DbContext.Transactions
                    .Where(w => w.Contract == deposit2.Contract && w.status != null).OrderBy(o => o.dateUpdate).ToList();
                /////////////////Rows
                int indexRow = 0;
                sheet.CreateRow(indexRow).Height = 300;
                int numberOfColumn = 3;
                //формируем заголовок
                sheet.GetRow(indexRow)
                    .CreateCell(0)
                    .SetCellValue(string.Format("Операции по депозиту счет: ".ToUpper() + deposit?.Account));
                sheet.GetRow(indexRow).GetCell(0).CellStyle = styleHeading;

                sheet.AddMergedRegion(new CellRangeAddress(indexRow, indexRow, 0, numberOfColumn));
                DateTime dataNav = DateTime.Now;
                decimal summa = deposit.Balance + deposit2.Balance;
                indexRow++; indexRow++;
                sheet.CreateRow(indexRow).Height = 300;
                //формируем заголовок
                sheet.GetRow(indexRow)
                    .CreateCell(0)
                    .SetCellValue(string.Format("По состоянию на " + dataNav.ToString("dd/MM/yyyy") + " Сумма вклада равна: " + summa));
                sheet.GetRow(indexRow).GetCell(0).CellStyle = styleHeading;

                sheet.AddMergedRegion(new CellRangeAddress(indexRow, indexRow, 0, numberOfColumn));

                indexRow++; indexRow++;
                sheet.CreateRow(indexRow).Height = 300;
                sheet.GetRow(indexRow)
                    .CreateCell(0)
                    .SetCellValue(string.Format("Депозит открыт : " + deposit?.DateBegin.ToString("dd/MM/yyyy")));
                sheet.GetRow(indexRow).GetCell(0).CellStyle = styleHeading;
                sheet.AddMergedRegion(new CellRangeAddress(indexRow, indexRow, 0, numberOfColumn));

                indexRow++; indexRow++;
                sheet.CreateRow(indexRow).Height = 300;
                sheet.GetRow(indexRow)
                    .CreateCell(0)
                    .SetCellValue(string.Format("Операции по депозиту: "));
                sheet.GetRow(indexRow).GetCell(0).CellStyle = styleHeading;
                sheet.AddMergedRegion(new CellRangeAddress(indexRow, indexRow, 0, numberOfColumn));

                indexRow++;
                sheet.CreateRow(indexRow).Height = 700;
                for (int i = 0; i < numberOfColumn + 1; i++)
                {
                    sheet.GetRow(indexRow).CreateCell(i).CellStyle = hederTableStyle;
                }
                sheet.GetRow(indexRow).GetCell(0).SetCellValue("№");
                sheet.GetRow(indexRow).GetCell(1).SetCellValue("Сумма дебет");
                sheet.GetRow(indexRow).GetCell(2).SetCellValue("Дата");
                sheet.GetRow(indexRow).GetCell(3).SetCellValue("Описание");

                ////width columns             
                sheet.SetColumnWidth(0, 3000);
                sheet.SetColumnWidth(1, 6000);
                sheet.SetColumnWidth(2, 6000);
                sheet.SetColumnWidth(3, 9000);

                var count = 0;
                foreach (var operation in listOperation)
                {
                    if (operation.Balance == 0)
                    {
                        continue;
                    }
                    DateTime date;
                    if (operation.dateUpdate != null)
                    {
                        date = (DateTime)operation.dateUpdate;
                        date = date.AddMonths(-1);
                    }
                    else
                    {
                        date = operation.DateCreat.AddMonths(-1);
                    }
                    indexRow++;
                    sheet.CreateRow(indexRow).Height = rowHeightCell;
                    
                    
                    if (count == 0)
                    {
                        for (int i = 0; i < numberOfColumn + 1; i++)
                        {
                            sheet.GetRow(indexRow).CreateCell(i).CellStyle = digitalStyle;
                        }
                        sheet.GetRow(indexRow).GetCell(0).SetCellValue(count);
                        sheet.GetRow(indexRow).GetCell(1).SetCellValue((double)deposit.Balance);
                        sheet.GetRow(indexRow).GetCell(2).SetCellValue("  " + deposit.DateBegin.ToString("dd/MM/yyyy"));
                        sheet.GetRow(indexRow).GetCell(3).SetCellValue("       Открытие вклада");
                        count++;
                        indexRow++;
                    }

                    sheet.CreateRow(indexRow).Height = rowHeightCell;
                    for (int i = 0; i < numberOfColumn + 1; i++)
                    {
                        sheet.GetRow(indexRow).CreateCell(i).CellStyle = digitalStyle;
                    }

                    sheet.GetRow(indexRow).GetCell(0).SetCellValue(count);
                    sheet.GetRow(indexRow).GetCell(1).SetCellValue((double)operation.Credit);
                        sheet.GetRow(indexRow).GetCell(2).SetCellValue("  " + date.ToString("dd/MM/yyyy"));
                        sheet.GetRow(indexRow).GetCell(3).SetCellValue("       Начисление процентов");
                    
                    count++;
                }

                workbook.Write(output);
                output.Close();
            }

            var result = File.ReadAllBytes(path);
            if (File.Exists(path))
                File.Delete(path);

            return result;
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
        private string AccountСreation(ref int chartAccountsId, DepositDto deposit)
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
            else
            {
                var сhartAccounts = _dalFactory.DbContext.ChartAccounts.FirstOrDefault(f => f.AccountNumber == "1523");
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

        private void InterestAccrual()
        {

            // начисление процентов в последний день месяца
            var allDipodits = _dalFactory.DbContext.Transactions.Where(w => w.status == null).ToList();
            for (int i = 0; i < allDipodits.Count(); i++)
            {
                var dateOpenDeposit = allDipodits[i].DateBegin;
                DateTime dateNav = DateTime.Now;
                var allMouth =
                    (MonthDifference(dateOpenDeposit, dateNav)) *
                    -1; // считаем кол-во месяцев месжу датами отклытия депозита и текущей
                if (allMouth > 0)
                {
                    for (int month = 0; month < allMouth; month++)
                    {
                        // найдем счет для начисления процентов 
                        Transaction interestAccount =
                            _dalFactory.DbContext.Transactions.Where(w => w.status == "%").AsEnumerable()
                                .FirstOrDefault(f =>
                                    f.Contract == allDipodits[i].Contract && f.dateUpdate == null);

                        //дата начисления процентов
                        var interestDate = (dateOpenDeposit.AddMonths(1 + month));
                        var firstDayOfMonth = new DateTime(interestDate.Year, interestDate.Month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // последний день месяца
                        if (!(lastDayOfMonth > DateTime.Now)) // проверяем что бы дата не была больше текущуй
                        {

                            // проверяем есть ли запись за этот месяц
                            bool interestСalculated = _dalFactory.DbContext.Transactions.Where(w => w.status == "%")
                                .AsEnumerable().Any(a =>
                                    a.Contract == allDipodits[i].Contract && a.dateUpdate == lastDayOfMonth);
                            decimal amountAccrual = 0;
                            if (!interestСalculated
                            ) // если такая запись найдена то проценты не начисляем (т.к они уже начислены)
                            {
                                // находим сумму начисленную по процентам за 1 месяц
                                amountAccrual = (allDipodits[i].Balance * allDipodits[i].InterestDeposit / 100) / 12;

                                // создаем новую транзакцию для начесления процентов (по месяцам)
                                // начисление процентов будет происходить в последний день месяца
                                if (interestAccount != null)
                                {
                                    var transaction = new Transaction
                                    {
                                        Account = interestAccount.Account,
                                        CurrensyId = interestAccount.CurrensyId,
                                        UserId = interestAccount.UserId,
                                        ChartAccountsId = interestAccount.ChartAccountsId,
                                        Balance = interestAccount.Balance +
                                                  amountAccrual, // итоговая сумма с начислениями процентов
                                        Contract = allDipodits[i].Contract,
                                        Credit = amountAccrual,
                                        DateBegin = allDipodits[i].DateBegin,
                                        DateCreat = DateTime.Now,
                                        DepositId = allDipodits[i].DepositId,
                                        DateEnd = allDipodits[i].DateEnd,
                                        Dedet = 0,
                                        InterestDeposit = allDipodits[i].InterestDeposit,
                                        status = "%"
                                    };
                                    _dalFactory.DbContext.Transactions
                                        .Add(transaction); // создаем новую запись с начисоеными процентами
                                    _dalFactory.DbContext.SaveChanges();

                                    interestAccount.dateUpdate = lastDayOfMonth;
                                    _dalFactory.DbContext.Transactions
                                        .Attach(interestAccount); // удаляем старую запись (путем обновления)
                                    _dalFactory.DbContext.Entry(interestAccount).State = EntityState.Modified;
                                    _dalFactory.DbContext.SaveChanges();
                                    BankDevelopmentAccount bankSumActualobj = _dalFactory.DbContext
                                        .BankDevelopmentAccount
                                        .FirstOrDefault(f => f.DateDelete == null);
                                    decimal bankSum = 0;
                                    if (bankSumActualobj != null)
                                    {
                                        bankSum = bankSumActualobj.Summ - amountAccrual;
                                    }

                                    if (bankSumActualobj != null)
                                        bankSumActualobj.DateDelete =
                                            DateTime.Now; // удаляем старую запись суммы банка (путем обновления)
                                    _dalFactory.DbContext.BankDevelopmentAccount.Attach(
                                        bankSumActualobj ?? throw new InvalidOperationException());
                                    _dalFactory.DbContext.Entry(bankSumActualobj).State = EntityState.Modified;
                                    _dalFactory.DbContext.SaveChanges();
                                    var newbankSumActualobj = new BankDevelopmentAccount
                                    {
                                        Summ = bankSum,
                                        DateUpdate = DateTime.Now
                                    };
                                    _dalFactory.DbContext.BankDevelopmentAccount.Add(newbankSumActualobj);
                                    _dalFactory.DbContext.SaveChanges();

                                }
                            }
                        }
                    }
                }

            }

        }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }
}
