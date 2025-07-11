using MediatR;

namespace BasketService.Application.UseCases.Commands.Baskets
{
    public record DeleteBasketCommand(Guid userId) : IRequest<Guid>
    {
    }
}
