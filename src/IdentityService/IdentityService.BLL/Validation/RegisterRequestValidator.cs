using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityService.BLL.Constants;
using IdentityService.BLL.DTO;

namespace IdentityService.BLL.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(ValidationRules.NameNotEmptyMessage)
                .MinimumLength(ValidationRules.NameMinLenght).WithMessage(ValidationRules.NameMinLenghtMessage)
                .MaximumLength(ValidationRules.NameMaxLenght).WithMessage(ValidationRules.NameMaxLenghtMessage);

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationRules.EmailNotEmptyMessage)
                .EmailAddress().WithMessage(ValidationRules.EmailErrorMessage);

            RuleFor(r => r.Password)
                .MinimumLength(ValidationRules.PasswordMinLenght).WithMessage(ValidationRules.PasswordMinLenghtMessage)
                .MaximumLength(ValidationRules.PasswordMaxLenght).WithMessage(ValidationRules.PasswordMaxLenghtMessage);
        }
    }
}
