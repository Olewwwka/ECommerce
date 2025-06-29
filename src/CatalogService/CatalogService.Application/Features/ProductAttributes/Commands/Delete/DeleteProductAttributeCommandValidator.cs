using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.ProductAttributes.Commands.Delete
{
    public class DeleteProductAttributeCommandValidator : AbstractValidator<DeleteProductAttributeCommand>
    {
        public DeleteProductAttributeCommandValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleFor(a => a.CategoryId)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
