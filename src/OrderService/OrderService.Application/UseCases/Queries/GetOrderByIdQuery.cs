using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Queries
{
    public record GetOrderByIdQuery(Guid orderId) : IRequest<OrderDto>
    {
    }
}
