using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Commands
{
    public record CreateOrderCommand(Guid UserId, CreateOrderDto Request) : IRequest<Guid>
    {
    }
}
