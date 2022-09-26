using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccountReconiliationManager : IAccountReconiliationService
    {
        private readonly IAccountReconiliationDal _accountReconiliationDal;

        public AccountReconiliationManager(IAccountReconiliationDal accountReconiliationDal)
        {
            _accountReconiliationDal = accountReconiliationDal;
        }
    }
}
