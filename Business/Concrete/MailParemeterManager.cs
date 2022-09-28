using Business.Abstract;
using Business.Constance;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MailParemeterManager : IMailParemeterService
    {
        private readonly IMailParemeterDal _mailParemeterDal;

        public MailParemeterManager(IMailParemeterDal mailParemeterDal)
        {
            _mailParemeterDal = mailParemeterDal;
        }

        public IDataResult<MailParemeter> Get(int companyId)
        {
            return new SuccesDataResult<MailParemeter>(_mailParemeterDal.Get(x => x.CompanyId == companyId));
        }

        public IResult Update(MailParemeter mailParemeter)
        {
            var result = Get(mailParemeter.CompanyId);
            if (result.Data == null)
            {
                _mailParemeterDal.Add(mailParemeter);           
            }
            else
            {
                result.Data.SMTP = mailParemeter.SMTP;
                result.Data.Port = mailParemeter.Port;
                result.Data.Email = mailParemeter.Email;
                result.Data.Password = mailParemeter.Password;
                result.Data.SSL = mailParemeter.SSL;
                _mailParemeterDal.Update(result.Data);
            }
            return new SuccessResult(Messages.MailParemeterUpdated);
        }
    }
}
