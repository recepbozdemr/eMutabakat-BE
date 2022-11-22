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
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService
    {

        private readonly IAccountReconciliationDetailDal _accountReconciliationDetailDal;
        public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal)
        {
            _accountReconciliationDetailDal = accountReconciliationDetailDal;
        }
        
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Add(AccountReconciliationDetail accountReconiliation)
        {
            _accountReconciliationDetailDal.Add(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationDetailAdd);
        }
        
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int accountReconiliationId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);

                        if (description != "Açıklama" && description != null)
                        {
                          
                            DateTime date = reader.GetDateTime(0);
                            double currencyId = reader.GetDouble(2);
                            double debit = reader.GetDouble(3);
                            double credit = reader.GetDouble(4);

                            AccountReconciliationDetail accountReconciliationDetail = new AccountReconciliationDetail()
                            {
                                AccountReconciliationId = accountReconiliationId,
                                Description = description,
                                CurrencyCredit = Convert.ToDecimal(credit),
                                CurrencyDebit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt16(currencyId)
                            };
                            _accountReconciliationDetailDal.Add(accountReconciliationDetail);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedReconiliationAccountAdd);
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Delete(AccountReconciliationDetail accountReconiliation)
        {
            _accountReconciliationDetailDal.Delete(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationAccountDelete);
        }

        [CacheAspect(60)]
        public IDataResult<AccountReconciliationDetail> GetById(int id)
        {
           return new SuccesDataResult<AccountReconciliationDetail>(_accountReconciliationDetailDal.Get(p => p.Id == id));
        }

        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliationDetail>> GetList(int accountReconiliationId)
        {
            return new SuccesDataResult<List<AccountReconciliationDetail>>(_accountReconciliationDetailDal.GetList(p => p.AccountReconciliationId == accountReconiliationId));
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Update(AccountReconciliationDetail accountReconiliation)
        {
            _accountReconciliationDetailDal.Update(accountReconiliation);
            return new SuccessResult(Messages.AddedReconiliationAccountUpdate);
        }
    }
}
