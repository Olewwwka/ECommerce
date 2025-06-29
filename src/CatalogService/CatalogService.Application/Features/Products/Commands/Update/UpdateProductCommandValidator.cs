using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.product.Name)
               .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
               .MaximumLength(ValidationConstants.MaxProductNameLenght)
               .WithMessage(ValidationConstants.MaxProductNameLenghtMessage);
            RuleFor(c => c.product.Description)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxProductDescriptionLenght)
                .WithMessage(ValidationConstants.MaxProductDescriptionLenghtMessage);
            RuleFor(c => c.product.Price)
                .GreaterThan(ValidationConstants.MinProductPrice)
                .WithMessage(ValidationConstants.MinProductPriceMessage);
        }
    }
}
