using MediatR;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.CommandHandlers
{
    /// <summary>
    /// Handler for ChangeOrderStatusCommand/>.
    /// Change existing order by updating status/>.
    /// </summary>
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Guid>
    {
        private readonly IOrderReposotory _orderRepository;
        public ChangeOrderStatusCommandHandler(IOrderReposotory orderRepository)
        {
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Handles the update of an order's status.
        /// </summary>
        /// <param name="request">Command containing order ID and new status.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>ID of the updated order.</returns>
        /// <exception cref="NotFoundException">
        /// Throws if order does not exist or if status is invalid.
        /// </exception>
        public async Task<Guid> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.orderId);

            if (existingOrder is null)
            {
                throw new NotFoundException($"Order with id {request.orderId} not found!");
            }

            if(!Enum.IsDefined(typeof(OrderStatus), request.newStatus))
            {
                throw new NotFoundException($"Status {request.newStatus} not found!");
            }

            var guid = await _orderRepository.UpdateStatusAsync(request.orderId, request.newStatus);

            return guid;
        }
    }
}
