using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Queries
{
    public record GetOrdersByUserIdQuery(Guid userId) : IRequest<List<OrderDto>>
    {
    }
}
