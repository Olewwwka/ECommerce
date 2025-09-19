using BasketService.Application.DTO;
using MediatR;

namespace BasketService.Application.UseCases.Queries.Baskets
{
    /// <summary>
    /// Query to get basket by user ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    public record GetBasketByUserIdQuery(Guid userId) : IRequest<BasketDto>
    {
    }
}
