using Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Şirket adı boş olamaz");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Şirket adı en az 2 karakter olmalıdır");
            RuleFor(c => c.TaxIdNumber).NotEmpty().WithMessage("Vergi numarası boş olamaz");
            RuleFor(c => c.TaxIdNumber).MinimumLength(10).WithMessage("Vergi numarası en az 10 karakter olmalıdır");
            RuleFor(c => c.TaxDepartment).NotEmpty().WithMessage("Vergi dairesi boş olamaz");
            RuleFor(c => c.TaxDepartment).MinimumLength(2).WithMessage("Vergi dairesi en az 2 karakter olmalıdır");
            RuleFor(c => c.IdentityNumber).NotEmpty().WithMessage("Vergi numarası boş olamaz");
            RuleFor(c => c.IdentityNumber).MinimumLength(11).WithMessage("Vergi numarası en az 11 karakter olmalıdır");
        }
    }
}
