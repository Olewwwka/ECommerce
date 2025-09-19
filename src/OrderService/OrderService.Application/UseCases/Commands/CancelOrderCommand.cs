using MediatR;

namespace OrderService.Application.UseCases.Commands
{
    /// <summary>
    /// Command to cancel existing order.
    /// </summary>
    /// <param name="orderId">Unique ID of order to cancel.</param>
    public record CancelOrderCommand(Guid orderId) : IRequest<Guid>
    {
    }
}
