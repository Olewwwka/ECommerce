using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Abstractions.Handlers;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Handlers
{
    public class PaymentFailedEventHandler : IPaymentFailedEventHandler
    {
        private readonly IOrderReposotory _repository;
        private readonly IEventProducer _producer;
        private readonly ILogger<PaymentFailedEventHandler> _logger;
        public PaymentFailedEventHandler(IOrderReposotory reposotory, IEventProducer producer, ILogger<PaymentFailedEventHandler> logger)
        {
            _producer = producer;
            _repository = reposotory;
            _logger = logger;
        }
        public async Task<PaymentResult> HandleAsync(PaymentFailedEvent paymentFailedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PaymentFailedEvent for Order ID: {OrderId}", paymentFailedEvent.OrderId);

            var existingOrder = await _repository.GetByIdAsync(paymentFailedEvent.OrderId);

            if(existingOrder is null)
            {
                throw new KeyNotFoundException($"Order with ID {paymentFailedEvent.OrderId} not found");
            }

            await _repository.UpdateStatusAsync(existingOrder.Id, OrderStatus.Rejected);

            var orderCancelledEvent = new OrderCancelledEvent
            {
                OrderId = existingOrder.Id,
                UserId = existingOrder.UserId,
                Reason = paymentFailedEvent.Message
            };

            await _producer.SendAsync(orderCancelledEvent, cancellationToken);

            _logger.LogInformation("Sent OrderCancelledEvent for Order ID: {OrderId} with reason: {Reason}", existingOrder.Id, paymentFailedEvent.Message);

            return new PaymentResult(existingOrder.Status, paymentFailedEvent.Message);
        }
    }
}
