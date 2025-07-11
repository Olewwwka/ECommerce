using BasketService.Application.UseCases.Commands.Baskets;
using FluentValidation;

namespace BasketService.Application.Validators
{
    public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
    {
        public UpdateBasketCommandValidator()
        {
            
        }
    }
}
