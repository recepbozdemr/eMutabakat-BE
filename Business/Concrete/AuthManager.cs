using Business.Abstract;
using Business.Constance;
using Core.Entities.Concrate;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrate;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyService _companyService;
        private readonly IMailParemeterService _mailParemeterService;
        private readonly IMailService _mailService;

      

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IMailParemeterService mailParemeterService, IMailService mailService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _mailParemeterService = mailParemeterService;
            _mailService = mailService;
        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyService.CompanyExists(company);
            if (result.Success == false)
            {
                return new ErrorResult(Messages.CompanyAlreadyExist);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
        {
            var claims = _userService.GetClaims(user, companyId);
            var accessToken = _tokenHelper.CreateToken(user , claims , companyId );
            return new SuccesDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
        }
   
        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToCheck = _userService.GetByMail(userForLogin.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password , userToCheck.PasswordHash , userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccesDataResult<User>(userToCheck , Messages.SuccessfulLogin);

        }

        public IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password , Company company)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password , out passwordHash , out passwordSalt);
            var user = new User
            {
                EMail = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name,
            };
          
            _userService.Add(user);
            _companyService.Add(company);
            _companyService.UserCompanyAdd(user.Id, company.Id);
            UserCompanyDto userCompanyDto = new UserCompanyDto()
            {
                Id = user.Id,
                Name = user.Name,
                EMail = user.EMail,
                CompanyId = company.Id,
                AddedAt = user.AddedAt,
                IsActive = true,
                MailConfirm = user.MailConfirm,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
            };
            var mailParameter = _mailParemeterService.Get(1);
            SendMailDto sendMailDto = new SendMailDto()
            {
                mailParemeter = mailParameter.Data,
                Mail = user.EMail,
                Subject = "Kullanıcı Onay Mail",
                Body = "Kayıt olundu linke tıkla"
            };
            _mailService.SendMail(sendMailDto);
            return new SuccesDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password)
        {
            throw new NotImplementedException();
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
