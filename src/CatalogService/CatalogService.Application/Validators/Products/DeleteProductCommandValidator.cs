using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Products
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
