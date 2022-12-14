using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Entities.Concrate;
using DataAccess.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {       
            _userDal.Add(user);
        }

        public User GetById(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(p=>p.EMail==email);
        }

        public User GetByMailConfirmValue(string value)
        {
            return _userDal.Get(p => p.MailConfirmValue == value);
        }

        public List<OperationsClaim> GetClaims(User user , int companyId)
        {
            return _userDal.GetClaims(user , companyId);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
