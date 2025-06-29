using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.Products.Commands.Delete
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
