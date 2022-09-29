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
        private readonly IMailTemplateService _mailTemplateService;

      

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IMailParemeterService mailParemeterService, IMailService mailService,IMailTemplateService mailTemplateService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _mailParemeterService = mailParemeterService;
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
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

        public IDataResult<User> GetById(int id)
        {
            return new SuccesDataResult<User>(_userService.GetById(id));
        }

        public IDataResult<User> GetByMailConfirmValue(string value)
        {
            return new SuccesDataResult<User>(_userService.GetByMailConfirmValue(value));
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
            SendConfirmEmail(user);
          
            return new SuccesDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        void SendConfirmEmail(User user)
        {
            string subject = "Kullanıcı Kayıt Onay Maili";
            string body = "Kullanıcı sisteme kayıt oldu. Kaydınızı tamamlamak için aşağıdaki linke tıklayınız.";
            string link = "https://localhost:7087/api/Auth/confirmUser?value=" + user.MailConfirmValue;
            string linkDescription = "Kaydı onaylamak için tıklayın";
            var mailTemplate = _mailTemplateService.GetByTemplateName("Kayit", 4);
            string templatebody = mailTemplate.Data.Value;
            templatebody = templatebody.Replace("{{title}}", subject);
            templatebody = templatebody.Replace("{{message}}", body);
            templatebody = templatebody.Replace("{{link}}", link);
            templatebody = templatebody.Replace("{{linkDescription}}", linkDescription);
            var mailParameter = _mailParemeterService.Get(4);
            SendMailDto sendMailDto = new SendMailDto()
            {
                mailParemeter = mailParameter.Data,
                Mail = user.EMail,
                Subject = "Kullanıcı Kayıt Onay Mail",
                Body = templatebody
            };
            
            _mailService.SendMail(sendMailDto);
            user.MailConfirmDate = DateTime.Now;
            _userService.Update(user);
        }

        public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password)
        {
            throw new NotImplementedException();
        }

        public IResult Update(User user)
        {
            _userService.Update(user);
            return new SuccessResult(Messages.UserMailConfirmeSuccess);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        IResult IAuthService.SendConfirmEmail(User user)
        {
            if (user.MailConfirm == true)
            {
                return new ErrorResult(Messages.MailAlready);
            }
            DateTime confirmMailDate = user.MailConfirmDate;
            DateTime now = DateTime.Now;
            if (confirmMailDate.ToShortDateString() == now.ToShortDateString())
            {
                if (confirmMailDate.Hour == now.Hour && confirmMailDate.AddMinutes(5).Minute <= now.Minute)
                {
                    SendConfirmEmail(user);
                    return new SuccessResult(Messages.MailSuccessFull);
                }
            }
            else
            {
                 return new ErrorResult(Messages.MailConfirmTimeHasNotExpired);
            }
            SendConfirmEmail(user);
            return new SuccessResult(Messages.MailSuccessFull);
        }
    }
}
