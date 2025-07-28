using BasketService.Application.DTO;
using MediatR;

namespace BasketService.Application.UseCases.Commands.Baskets
{
    public record UpdateBasketCommand(Guid userId, BasketDto basket) : IRequest<BasketDto>
    {
    }
}
