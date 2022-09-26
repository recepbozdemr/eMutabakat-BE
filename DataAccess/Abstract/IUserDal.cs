using Core.DataAccess;
using Core.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {

        List<OperationsClaim>GetClaims(User user , int companyId);
    }
}
