using CatalogService.Application.UseCases.Commands.Brands;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Brands
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(b => b.Name)
                  .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                  .MaximumLength(ValidationConstants.MaxBrandNameLenght).WithMessage(ValidationConstants.MaxBrandNameLenghtMessage);
        }
    }
}
