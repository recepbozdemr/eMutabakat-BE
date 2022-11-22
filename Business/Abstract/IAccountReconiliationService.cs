using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountReconiliationService
    {
        IResult Add(AccountReconiliation accountReconiliation);

        IResult AddToExcel(string filePath, int companyId);

        IResult Update(AccountReconiliation accountReconiliation);

        IResult Delete(AccountReconiliation accountReconiliation);

        IDataResult<List<AccountReconiliation>> GetList(int companyId);

        IDataResult<AccountReconiliation> GetById(int id);
    }
}
