using Core.Entities.Concrate;
using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {

        IResult Add(Company company);

        IDataResult<List<Company>> GetList();

        IDataResult<UserCompany> GetCompany(int userId);

        IResult CompanyExists(Company company);

        IResult UserCompanyAdd(int userId, int companyId);
    }
}
