using CatalogService.Application.UseCases.Commands.Categories;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Categories
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
