using FluentValidation;
using IdentityService.BLL.Constants;
using IdentityService.BLL.DTO;

namespace IdentityService.BLL.Validation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationRules.EmailNotEmptyMessage)
                .EmailAddress().WithMessage(ValidationRules.EmailErrorMessage);

            RuleFor(r => r.Password)
                .MinimumLength(ValidationRules.PasswordMinLenght).WithMessage(ValidationRules.PasswordMinLenghtMessage)
                .MaximumLength(ValidationRules.PasswordMaxLenght).WithMessage(ValidationRules.PasswordMaxLenghtMessage);
        }
    }
}
