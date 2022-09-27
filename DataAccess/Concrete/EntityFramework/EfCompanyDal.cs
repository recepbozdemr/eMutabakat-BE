using Core.DataAccess.EntityFramework;
using Core.Entities.Concrate;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, ContextDb>, ICompanyDal
    {
        public void UserCompanyAdd(int userId, int companyId)
        {
            using (var context = new ContextDb()) {

                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = companyId,
                    AddedAt = DateTime.Now,
                    IsActive = true
                };
                context.UserCompanies.Add(userCompany);
            }
        }
    }
}
