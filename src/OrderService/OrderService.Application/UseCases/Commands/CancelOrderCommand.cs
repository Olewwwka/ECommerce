using MediatR;

namespace OrderService.Application.UseCases.Commands
{
    public record CancelOrderCommand(Guid orderId) : IRequest<Guid>
    {
    }
}
