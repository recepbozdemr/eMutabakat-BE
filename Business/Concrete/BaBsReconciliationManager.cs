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
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal _baBsReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;

        public BaBsReconciliationManager(IBaBsReconciliationDal baBsReconciliationDal, ICurrencyAccountService currencyAccountService)
        {
            _baBsReconciliationDal = baBsReconciliationDal;
            _currencyAccountService = currencyAccountService;
        }

        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Add(BaBsReconciliation accountReconiliation)
        {
            _baBsReconciliationDal.Add(accountReconiliation);
            return new SuccessResult(Messages.BaBsReconciliationAdd);
        }

        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
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
                            string type = reader.GetString(1);
                            double mounth = reader.GetDouble(2);
                            double year = reader.GetDouble(3);
                            double quantit = reader.GetDouble(4);
                            double total = reader.GetDouble(5);
                            int currencyAccountId = _currencyAccountService.GetByCode(code, companyId).Data.Id;


                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = Convert.ToInt16(mounth),
                                Year = Convert.ToInt16(year),
                                Quantity = Convert.ToInt16(quantit),
                                Total = Convert.ToDecimal(total)
                            };
                            _baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
                File.Delete(filePath);
                return new SuccessResult(Messages.BaBsReconciliationAdd);
            }
        }

        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Delete(BaBsReconciliation accountReconiliation)
        {
            _baBsReconciliationDal.Delete(accountReconiliation);
            return new SuccessResult(Messages.BaBsReconciliationDelete);
        }

        [CacheAspect(60)]
        public IDataResult<BaBsReconciliation> GetById(int id)
        {
            return new SuccesDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(p => p.Id == id));
        }

        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliation>> GetList(int companyId)
        {
            return new SuccesDataResult<List<BaBsReconciliation>>(_baBsReconciliationDal.GetList(p => p.CompanyId == companyId));
        }

        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Update(BaBsReconciliation accountReconiliation)
        {
            _baBsReconciliationDal.Update(accountReconiliation);
            return new SuccessResult(Messages.BaBsReconciliationUpdate);
        }
    }
}
