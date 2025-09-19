using MediatR;

namespace BasketService.Application.UseCases.Commands.Baskets
{
    /// <summary>
    /// Command to delete user basket.
    /// </summary>
    /// <param name="userId">User ID.</param>
    public record DeleteBasketCommand(Guid userId) : IRequest<Guid>
    {
    }
}
