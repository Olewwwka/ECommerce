using MediatR;
using OrderService.Domain.Enums;

namespace OrderService.Application.UseCases.Commands
{
    /// <summary>
    /// Command to change status of existing order.
    /// </summary>
    /// <param name="orderId">Unique ID of order.</param>
    /// <param name="newStatus">New status to apply to order.</param>
    public record ChangeOrderStatusCommand(Guid orderId, OrderStatus newStatus) : IRequest<Guid>
    {
    }
}
