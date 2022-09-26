using Business.Abstract;
using DataAccess.Abstract;
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
    }
}
