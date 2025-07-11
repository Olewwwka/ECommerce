using FluentValidation;
using OrderService.Application.UseCases.Commands;

namespace OrderService.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.UserId)
                .NotEmpty()
                .WithMessage("");

            RuleForEach(o => o.Request.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("");
                items.RuleFor(i => i.Price)
                    .NotEmpty().WithMessage("")
                    .GreaterThan(0);
                items.RuleFor(i => i.Count)
                    .NotEmpty().WithMessage("")
                    .GreaterThan(0);
            });
        }
    }
}
