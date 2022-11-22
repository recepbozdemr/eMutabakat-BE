using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountReconciliationDetailService 
    {
        IResult Add(AccountReconciliationDetail accountReconiliation);

        IResult AddToExcel(string filePath, int companyId);

        IResult Update(AccountReconciliationDetail accountReconiliation);

        IResult Delete(AccountReconciliationDetail accountReconiliation);

        IDataResult<List<AccountReconciliationDetail>> GetList(int accountReconiliationId);

        IDataResult<AccountReconciliationDetail> GetById(int id);
    }
}
