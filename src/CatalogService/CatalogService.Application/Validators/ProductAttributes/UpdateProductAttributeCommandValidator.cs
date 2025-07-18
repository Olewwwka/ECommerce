using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.ProductAttributes
{
    public class UpdateProductAttributeCommandValidator : AbstractValidator<UpdateProductAttributeCommand>
    {
        public UpdateProductAttributeCommandValidator()
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
