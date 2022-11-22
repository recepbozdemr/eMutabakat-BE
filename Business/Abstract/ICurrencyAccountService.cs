using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICurrencyAccountService
    {
        IResult Add(CurrencyAccount currencyAcount);

        IResult AddToExcel(string fileName , int companyId);

        IResult Update(CurrencyAccount currencyAcount);

        IResult Delete(CurrencyAccount currencyAcount);

        IDataResult<CurrencyAccount> Get(int id);

        IDataResult<List<CurrencyAccount>> GetList(int companyId);

        IDataResult<CurrencyAccount> GetByCode(string code, int companyId);
    }
}
