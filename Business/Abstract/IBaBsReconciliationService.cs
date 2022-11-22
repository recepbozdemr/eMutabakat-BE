using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBaBsReconciliationService
    {
        IResult Add(BaBsReconciliation accountReconiliation);

        IResult AddToExcel(string filePath, int companyId);

        IResult Update(BaBsReconciliation accountReconiliation);

        IResult Delete(BaBsReconciliation accountReconiliation);

        IDataResult<List<BaBsReconciliation>> GetList(int companyId);

        IDataResult<BaBsReconciliation> GetById(int id);
    }
}
