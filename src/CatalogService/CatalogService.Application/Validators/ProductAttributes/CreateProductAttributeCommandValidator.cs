using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.ProductAttributes
{
    public class CreateProductAttributeCommandValidator : AbstractValidator<CreateProductAttributeCommand>
    {
        public CreateProductAttributeCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage)
                .MaximumLength(ValidationConstants.MaxProductAttributeNameLenght)
                .WithMessage(ValidationConstants.MaxProductAttributeNameLenghtMessage);

            RuleFor(a => a.CategoryId)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
