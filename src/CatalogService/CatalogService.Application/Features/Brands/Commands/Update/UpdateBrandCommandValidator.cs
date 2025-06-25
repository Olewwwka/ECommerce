using FluentValidation;

namespace CatalogService.Application.Features.Brands.Commands.Update
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(b => b.Name)
             .NotEmpty().WithMessage("")
             .MaximumLength(50).WithMessage("");
        }
    }
}
