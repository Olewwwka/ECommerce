using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Features.Brands.Commands.Delete
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage(ValidationConstants.NoEmptyMessage);
        }
    }
}
