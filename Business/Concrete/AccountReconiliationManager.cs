using Business.Abstract;
using Business.Constance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrate;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccountReconiliationManager : IAccountReconiliationService
    {
        private readonly IAccountReconiliationDal _accountReconiliationDal;
        private readonly ICurrencyAccountService _currecyAccountService;

        public AccountReconiliationManager(IAccountReconiliationDal accountReconiliationDal , ICurrencyAccountService currecyAccountService)
        {
            _accountReconiliationDal = accountReconiliationDal;
            _currecyAccountService = currecyAccountService;
        }
        [CacheRemoveAspect("IAccountReconiliationService.Get")]
        public IResult Add(AccountReconiliation accountReconiliation)
        {
            _accountReconiliationDal.Add(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationAccountAdd);
               
        }

        [CacheRemoveAspect("IAccountReconiliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);

                        if (code != "Cari Kodu" && code != null)
                        {
                            DateTime startingDate = reader.GetDateTime(1);
                            DateTime endingDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double debit = reader.GetDouble(4);
                            double credit = reader.GetDouble(5);

                            int currencyAccountId = _currecyAccountService.GetByCode(code, companyId).Data.Id;
                            string guid = Guid.NewGuid().ToString();

                            AccountReconiliation accountReconciliation = new AccountReconiliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyCredit = Convert.ToDecimal(credit),
                                CurrencyDebit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt16(currencyId),
                                StartingDate = startingDate,
                                EndingDate = endingDate 
                            };
                            _accountReconiliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedReconiliationAccountAdd);
        }
        
        [CacheRemoveAspect("IAccountReconiliationService.Get")]
        public IResult Delete(AccountReconiliation accountReconiliation)
        {
            _accountReconiliationDal.Delete(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationAccountDelete);
        }
        [CacheAspect(60)]
        public IDataResult<AccountReconiliation> GetById(int id)
        {
            return new SuccesDataResult<AccountReconiliation>(_accountReconiliationDal.Get(p => p.Id == id));
        }

        [CacheAspect(60)]
        public IDataResult<List<AccountReconiliation>> GetList(int companyId)
        {
            return new SuccesDataResult<List<AccountReconiliation>>(_accountReconiliationDal.GetList(p => p.CompanyId == companyId));
        }

        public IResult Update(AccountReconiliation accountReconiliation)
        {
            _accountReconiliationDal.Update(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationAccountUpdate);
        }
    }
}
