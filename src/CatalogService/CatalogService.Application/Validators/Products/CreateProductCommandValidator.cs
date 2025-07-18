using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxProductNameLenght)
                .WithMessage(ValidationConstants.MaxProductNameLenghtMessage);
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxProductDescriptionLenght)
                .WithMessage(ValidationConstants.MaxProductDescriptionLenghtMessage);
            RuleFor(c => c.Price)
                .GreaterThan(ValidationConstants.MinProductPrice)
                .WithMessage(ValidationConstants.MinProductPriceMessage);
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
            RuleFor(c => c.BrandId)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);

            RuleForEach(c => c.AttributeValues).ChildRules(a =>
            {
                a.RuleFor(a => a.ProductAttributeId)
                    .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
                a.RuleFor(a => a.Value)
                    .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                    .MaximumLength(ValidationConstants.MaxProductAttributeValueLenght)
                    .WithMessage(ValidationConstants.MaxProductAttributeValueLenghtMessage);
            });
        }
    }
}
