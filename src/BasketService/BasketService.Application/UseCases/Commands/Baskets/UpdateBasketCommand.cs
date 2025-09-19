using BasketService.Application.DTO;
using MediatR;

namespace BasketService.Application.UseCases.Commands.Baskets
{
    /// <summary>
    /// Command to update user basket.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="basket">Basket with updated items.</param>
    public record UpdateBasketCommand(Guid userId, BasketDto basket) : IRequest<BasketDto>
    {
    }
}
