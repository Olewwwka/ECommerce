using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Delete
{
    public class DeleteProductAttributeValueCommandValidator : AbstractValidator<DeleteProductAttributeValueCommand>
    {
        public DeleteProductAttributeValueCommandValidator()
        {
            RuleFor(a => a.ProductId)
               .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleFor(a => a.ProductAttributeId)
               .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
