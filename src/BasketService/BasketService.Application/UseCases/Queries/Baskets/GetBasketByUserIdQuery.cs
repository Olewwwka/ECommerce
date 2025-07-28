using BasketService.Application.DTO;
using MediatR;

namespace BasketService.Application.UseCases.Queries.Baskets
{
    public record GetBasketByUserIdQuery(Guid userId) : IRequest<BasketDto>
    {
    }
}
