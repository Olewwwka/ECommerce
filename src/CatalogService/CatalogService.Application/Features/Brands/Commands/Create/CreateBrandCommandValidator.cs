using FluentValidation;

namespace CatalogService.Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("")
                .MaximumLength(50).WithMessage("");
        }
    }
}
