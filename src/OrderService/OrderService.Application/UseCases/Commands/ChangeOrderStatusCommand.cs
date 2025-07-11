using MediatR;
using OrderService.Application.DTO;
using OrderService.Domain.Enums;

namespace OrderService.Application.UseCases.Commands
{
    public record ChangeOrderStatusCommand(Guid orderId, OrderStatus newStatus) : IRequest<Guid>
    {
    }
}
