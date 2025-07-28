using FluentValidation;
using OrderService.Application.UseCases.Commands;

namespace OrderService.Application.Validators
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(o => o.orderId)
                .NotEmpty().WithMessage("");
        }
    }
}
