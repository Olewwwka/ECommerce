using MediatR;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.CommandHandlers
{
    /// <summary>
    /// Handler for CancelOrderCommand/>.
    /// Cancel existing order by updating status/>.
    /// </summary>
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Guid>
    {
        private readonly IOrderReposotory _orderRepository;
        public CancelOrderCommandHandler(IOrderReposotory orderRepository)
        {
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Handle the cancellation of an order.
        /// </summary>
        /// <param name="request">Cancel order command with order ID.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>ID of cancelled order.</returns>
        /// <exception cref="NotFoundException">Throws if order does not exist.</exception>
        public async Task<Guid> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.orderId);

            if(existingOrder is null)
            {
                throw new NotFoundException($"Order with id {request.orderId} not found!");
            }

            var result = await _orderRepository.UpdateStatusAsync(request.orderId, OrderStatus.Rejected);

            return result;
        }


    }
}
