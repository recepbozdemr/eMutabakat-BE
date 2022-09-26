﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrate;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ContextDb>, IUserDal
    {
        public List<OperationsClaim> GetClaims(User user, int companyId)
        {
            using (var context = new ContextDb())
            {
                var result = from operationClaim in context.OperationsClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
                             select new OperationsClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                return result.ToList();

            }
        }
    }
}
