﻿using Business.Abstract;
using Core.Entities.Concrate;
using DataAccess.Abstract;
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

        public void Add(User user)
        {
           _userDal.Add(user);
        }


        public User GetByMail(string email)
        {
            return _userDal.Get(p=>p.EMail==email);
        }

        public List<OperationsClaim> GetClaims(User user , int companyId)
        {
            return _userDal.GetClaims(user , companyId);
        }
    }
}
