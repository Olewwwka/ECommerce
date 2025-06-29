using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.Categories.Commands.Delete
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
