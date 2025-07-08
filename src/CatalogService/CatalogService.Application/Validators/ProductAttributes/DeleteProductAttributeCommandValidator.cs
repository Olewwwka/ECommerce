using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.ProductAttributes
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
