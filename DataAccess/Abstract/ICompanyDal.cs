using Core.DataAccess;
using Core.Entities.Concrate;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        void UserCompanyAdd(int userId, int companyId);
        
        UserCompany GetCompanyList(int userId);
    }
}
