using CatalogService.Application.UseCases.Commands.ProductAttributeValues;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validatorss.ProductAttributeValues
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
