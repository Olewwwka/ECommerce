using CatalogService.Application.UseCases.Commands.Categories;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Categories
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxCategoryNameLenght).WithMessage(ValidationConstants.MaxCategoryNameLenghtMessage);
        }
    }
}
