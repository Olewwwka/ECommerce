using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.Categories.Commands.Create
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
