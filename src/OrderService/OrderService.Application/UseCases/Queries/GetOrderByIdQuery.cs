using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Queries
{
    /// <summary>
    /// Query to get order by ID.
    /// </summary>
    /// <param name="orderId">Unique ID of the order.</param>
    public record GetOrderByIdQuery(Guid orderId) : IRequest<OrderDto>
    {
    }
}
