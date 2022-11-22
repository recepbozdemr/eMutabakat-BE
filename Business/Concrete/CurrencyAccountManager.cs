using Business.Abstract;
using Business.Constance;
using Business.Validation.FluentValidation;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
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
    public class CurrencyAccountManager : ICurrencyAccountService
    {
        private readonly ICurrencyAccountDal _currencyAccountDal;

        public CurrencyAccountManager(ICurrencyAccountDal currencyAccountDal)
        {
            _currencyAccountDal = currencyAccountDal;
        }

        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Add(CurrencyAccount currencyAcount)
        {
            _currencyAccountDal.Add(currencyAcount);
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath , int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // For Turkish Characters

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        string name = reader.GetString(1);
                        string address = reader.GetString(2);
                        string taxDepartment = reader.GetString(3);
                        string taxNumber = reader.GetString(4);
                        string identityNumber = reader.GetString(5);
                        string email = reader.GetString(6);
                        string authorized = reader.GetString(7);

                        if (code != "Cari Kodu")
                        {
                            CurrencyAccount currencyAccount = new CurrencyAccount
                            {
                                Code = code,
                                Name = name,
                                Address = address,
                                TaxDepartment = taxDepartment,
                                TaxIdNumber = taxNumber,
                                IdentityNumber = identityNumber,
                                Email = email,
                                Authorized = authorized,
                                IsActive = true,
                                AddedAt = DateTime.Now,
                                CompanyId = companyId

                            };

                            _currencyAccountDal.Add(currencyAccount);

                        }

                    }

                }
            }

            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        public IResult Delete(CurrencyAccount currencyAcount)
        {
            _currencyAccountDal.Delete(currencyAcount);
            return new SuccessResult(Messages.AddedCurrencyAccountDelete);
        }

        [CacheAspect(60)]
        public IDataResult<CurrencyAccount> Get(int id)
        {
            return new SuccesDataResult<CurrencyAccount>(_currencyAccountDal.Get(p => p.Id == id));
        }

        [CacheAspect(60)]
        public IDataResult<CurrencyAccount> GetByCode(string code, int companyId)
        {
            return new SuccesDataResult<CurrencyAccount>(_currencyAccountDal.Get(p => p.Code == code && p.CompanyId == companyId));
        }

        [CacheAspect(60)]
        public IDataResult<List<CurrencyAccount>> GetList(int companyId)
        {
            return new SuccesDataResult<List<CurrencyAccount>>(_currencyAccountDal.GetList(p => p.CompanyId == companyId));
        }
        [ValidationAspect(typeof(CurrencyAccountValidator))]

        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        public IResult Update(CurrencyAccount currencyAcount)
        {
            _currencyAccountDal.Update(currencyAcount);
            return new SuccessResult(Messages.AddedCurrencyAccountUpdate);
        }
    }
}