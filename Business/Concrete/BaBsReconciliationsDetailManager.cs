using Business.Abstract;
using Business.Constance;
using Core.Aspect.Autofac.Transaction;
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
    public class BaBsReconciliationsDetailManager : IBaBsReconciliationsDetailService
    {
        private readonly IBaBsReconciliationsDetailDal _baBsReconciliationsDetailDal;

        public BaBsReconciliationsDetailManager(IBaBsReconciliationsDetailDal baBsReconciliationsDetailDal)
        {
            _baBsReconciliationsDetailDal = baBsReconciliationsDetailDal;
        }

        public IResult Add(BaBsReconciliationsDetail baBsReconciliationsDetail)
        {
            _baBsReconciliationsDetailDal.Add(baBsReconciliationsDetail);
            return new SuccessResult(Messages.BaBsReconciliationDetailAdd);
        }
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int baBsReconciliationId)
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
                            double amount = reader.GetDouble(2);

                            BaBsReconciliationsDetail baBsReconciliationsDetail = new BaBsReconciliationsDetail()
                            {
                                BaBsReconciliationsId = baBsReconciliationId,
                                Description = description,
                                Amount = Convert.ToDecimal(amount),
                                Date = date
                            };
                            _baBsReconciliationsDetailDal.Add(baBsReconciliationsDetail);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.BaBsReconciliationDetailAdd);
        }

        public IResult Delete(BaBsReconciliationsDetail baBsReconciliationsDetail)
        {
            _baBsReconciliationsDetailDal.Delete(baBsReconciliationsDetail);
            return new SuccessResult(Messages.BaBsReconciliationDetailDelete);
        }

        public IDataResult<BaBsReconciliationsDetail> GetById(int id)
        {
            return new SuccesDataResult<BaBsReconciliationsDetail>(_baBsReconciliationsDetailDal.Get(p => p.Id == id));
        }

        public IDataResult<List<BaBsReconciliationsDetail>> GetList(int baBsReconciliationsDetail)
        {
            return new SuccesDataResult<List<BaBsReconciliationsDetail>>(_baBsReconciliationsDetailDal.GetList(p => p.BaBsReconciliationsId == baBsReconciliationsDetail));
        }

        public IResult Update(BaBsReconciliationsDetail baBsReconciliationsDetail)
        {
            _baBsReconciliationsDetailDal.Update(baBsReconciliationsDetail);
            return new SuccessResult(Messages.BaBsReconciliationDetailDelete);
        }
    }
}
