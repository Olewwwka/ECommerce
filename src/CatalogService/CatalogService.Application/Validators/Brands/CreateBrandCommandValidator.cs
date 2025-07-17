using CatalogService.Application.UseCases.Commands.Brands;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Brands
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxBrandNameLenght).WithMessage(ValidationConstants.MaxBrandNameLenghtMessage);
        }
    }
}
