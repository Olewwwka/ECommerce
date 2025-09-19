using MediatR;

namespace BasketService.Application.UseCases.Commands.Baskets
{
    /// <summary>
    /// Command to add item to user basket.
    /// </summary>
    /// <param name="UserId">User ID.</param>
    /// <param name="ProductId">Product ID</param>
    /// <param name="Quantity">Quantity of product.</param>
    public record AddItemToBasketCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest<Guid>
    {
    }
}
