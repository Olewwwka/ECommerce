using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Abstractions.Handlers;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Handlers
{
    public class PaymentSucceedEventHandler : IPaymentSucceedEventHandler
    {
        private readonly IOrderReposotory _repository;
        private readonly IEventProducer _producer;
        private readonly ILogger<PaymentSucceedEventHandler> _logger;
        public PaymentSucceedEventHandler(IOrderReposotory reposotory, IEventProducer producer, ILogger<PaymentSucceedEventHandler> logger)
        {
            _logger = logger;
            _repository = reposotory;
            _producer = producer;
        }
        public async Task<PaymentResult> HandleAsync(PaymentSucceedEvent paymentSucceedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PaymentSucceedEvent for Order ID: {OrderId}", paymentSucceedEvent.OrderId);

            var existingOrder = await _repository.GetByIdAsync(paymentSucceedEvent.OrderId);

            if (existingOrder is null)
            {
                throw new KeyNotFoundException($"Order with ID {paymentSucceedEvent.OrderId} not found");
            }

            try
            {
                //if (true)
                //{
                //    throw new Exception();
                //}
                await _repository.UpdateStatusAsync(existingOrder.Id, OrderStatus.Completed);

                var orderCompletedEvent = new OrderCompletedEvent
                {
                    OrderId = existingOrder.Id,
                    UserId = existingOrder.UserId,
                    CompletedAt = DateTime.UtcNow,
                };

                await _producer.SendAsync(orderCompletedEvent, cancellationToken);

                _logger.LogInformation("Sent OrderCompletedEvent for Order ID: {OrderId}", existingOrder.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update order status for Order ID: {OrderId}", existingOrder.Id);

                var orderFailedEvent = new OrderFailedEvent(paymentSucceedEvent.OrderId, paymentSucceedEvent.UserId, "Failed to update order status", DateTime.UtcNow);

                await _producer.SendAsync(orderFailedEvent, cancellationToken);

                _logger.LogInformation("Sent OrderFailedEvent for Order ID: {OrderId}", existingOrder.Id);
            }


            return new PaymentResult(existingOrder.Status, paymentSucceedEvent.Amount.ToString());
        }
    }
}
