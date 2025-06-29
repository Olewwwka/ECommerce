using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Create
{
    public class CreateProductAttributeValueValidator : AbstractValidator<CreateProductAttributeValueCommand>
    {
        public CreateProductAttributeValueValidator()
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
