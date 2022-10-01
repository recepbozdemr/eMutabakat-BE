using Core.Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class UserValidator  : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(u => u.Name).MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter olmalıdır");
            RuleFor(u => u.EMail).NotEmpty().WithMessage("Mail boş olamaz");
            RuleFor(u => u.EMail).EmailAddress().WithMessage("Geçerli bir mail adresi yazın");
          
        }
        
    }
}
