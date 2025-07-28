using BasketService.Application.UseCases.Queries.Baskets;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class GetBasketByUserIdQueryValidator : AbstractValidator<GetBasketByUserIdQuery>
    {
        public GetBasketByUserIdQueryValidator()
        {
            RuleFor(r => r.userId)
               .NotNull().WithMessage("");
        }
    }
}
