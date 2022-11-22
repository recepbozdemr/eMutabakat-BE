using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBaBsReconciliationsDetailService
    {
        IResult Add(BaBsReconciliationsDetail baBsReconciliationsDetail);

        IResult AddToExcel(string filePath, int companyId);

        IResult Update(BaBsReconciliationsDetail baBsReconciliationsDetail);

        IResult Delete(BaBsReconciliationsDetail baBsReconciliationsDetail);

        IDataResult<List<BaBsReconciliationsDetail>> GetList(int baBsReconciliationsDetail);

        IDataResult<BaBsReconciliationsDetail> GetById(int id);
    }
}
