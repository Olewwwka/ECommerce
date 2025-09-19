using MediatR;
using OrderService.Application.DTO;

namespace OrderService.Application.UseCases.Commands
{
    /// <summary>
    /// Command to create new order for user.
    /// </summary>
    /// <param name="UserId">Unique ID of the user.</param>
    /// <param name="Request">Details of the order.</param>
    public record CreateOrderCommand(Guid UserId, CreateOrderDto Request) : IRequest<Guid>
    {
    }
}
