using CatalogService.Application.UseCases.Commands.Categories;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Categories
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxCategoryNameLenght).WithMessage(ValidationConstants.MaxCategoryNameLenghtMessage);
        }
    }
}
