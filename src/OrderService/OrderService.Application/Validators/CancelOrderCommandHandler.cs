using FluentValidation;
using OrderService.Application.UseCases.Commands;

namespace OrderService.Application.Validators
{
    public class CancelOrderCommandHandler : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandHandler()
        {
            RuleFor(o => o.orderId)
                .NotEmpty().WithMessage("");
        }
    }
}
