using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.CommandHandlers
{
    /// <summary>
    /// Handler for CreateOrderCommand/>.
    /// Create new order/>.
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOrderReposotory _orderRepository;
        private readonly IEventProducer _eventProducer;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(IMapper mapper, IOrderReposotory orderRepository, IEventProducer eventProducer, ILogger<CreateOrderCommandHandler> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _eventProducer = eventProducer;
        }
        /// <summary>
        /// Handle order creation.
        /// </summary>
        /// <param name="request">Command containing order details and user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>ID of the created order.</returns>
        /// <exception cref="UserHasUnpaidOrderException">
        /// Throws if user already has unpaid order.
        /// </exception>
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrders = await _orderRepository.GetByUserId(request.UserId);

            if(existingOrders.Any(o => o.Status == OrderStatus.Pending))
            {
                throw new UserHasUnpaidOrderException($"User with id {request.UserId} have unpaid order!");
            }

            var order = _mapper.Map<Order>(request);

            order.TotalPrice = order.Items.Sum(x => x.Price * x.Count);

            var savedOrder = await _orderRepository.CreateAsync(order);

            var orderCreatedEvent = new OrderCreatedEvent(savedOrder, request.UserId, order.TotalPrice);

            await _eventProducer.SendAsync(orderCreatedEvent, cancellationToken);

            _logger.LogInformation($"Sent orderCreatedEvent: UserId: {orderCreatedEvent.UserId}, OrderId: {orderCreatedEvent.OrderId}");

            return savedOrder;
        }
    }
}
