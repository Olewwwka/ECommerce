using BasketService.Application.UseCases.Commands.Baskets;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(r => r.userId)
                .NotNull().WithMessage("");
        }
    }
}
