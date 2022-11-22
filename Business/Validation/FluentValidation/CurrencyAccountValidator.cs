using Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    internal class CurrencyAccountValidator : AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Firma adı en az 2 karakter olmalıdır");
            RuleFor(c => c.Address).NotEmpty().WithMessage("Firma adı boş geçilemez");
            RuleFor(c => c.Address).MinimumLength(50).WithMessage("Firma adresi en az 50 karakter olmalıdır");
        }
    }
}
