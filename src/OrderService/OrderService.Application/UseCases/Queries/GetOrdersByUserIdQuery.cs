using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Queries
{
    /// <summary>
    /// Query to get all orders for user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    public record GetOrdersByUserIdQuery(Guid userId) : IRequest<List<OrderDto>>
    {
    }
}
