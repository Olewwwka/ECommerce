using CatalogService.Application.UseCases.Commands.Brands;
using CatalogService.Domain.Constants;
using FluentValidation;

namespace CatalogService.Application.Validators.Brands
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
