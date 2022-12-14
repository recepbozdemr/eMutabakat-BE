using Core.Utilities.Results.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMailParemeterService
    {
        IResult Update(MailParemeter mailParemeter);
        
        IDataResult<MailParemeter> Get(int companyId);
    }
}
