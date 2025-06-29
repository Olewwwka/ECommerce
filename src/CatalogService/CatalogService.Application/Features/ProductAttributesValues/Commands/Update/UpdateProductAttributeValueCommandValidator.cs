using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Update
{
    public class UpdateProductAttributeValueCommandValidator : AbstractValidator<UpdateProductAttributeValueCommand>
    {
        public UpdateProductAttributeValueCommandValidator()
        {
            RuleFor(a => a.ProductId)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleFor(a => a.ProductAttributeId)
               .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleFor(a => a.Value)
               .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
               .MaximumLength(ValidationConstants.MaxProductAttributeValueLenght)
               .WithMessage(ValidationConstants.MaxProductAttributeValueLenghtMessage);
        }
    }
}
