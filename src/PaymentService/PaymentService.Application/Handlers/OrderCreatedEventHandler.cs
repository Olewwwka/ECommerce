using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using PaymentService.Application.Abstractions;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Abstractions.Handlers;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace PaymentService.Application.Handlers
{
    public class OrderCreatedEventHandler : IOrderCreatedEventHandler
    {
        private readonly IWithdrawBallanceService _ballanceService;
        private readonly IEventProducer _eventPublisher;
        private readonly IReceiptRepository _receiptRepository;
        private readonly ILogger<OrderCreatedEventHandler> _logger;
        public OrderCreatedEventHandler(IWithdrawBallanceService ballanceService, IEventProducer eventPublisher, IReceiptRepository receiptRepository, ILogger<OrderCreatedEventHandler> logger)
        {
            _ballanceService = ballanceService;
            _eventPublisher = eventPublisher;
            _receiptRepository = receiptRepository;
            _logger = logger;
        }
        public async Task HandleAsync(OrderCreatedEvent orderCreatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling OrderCreatedEvent for Order ID: {OrderId}", orderCreatedEvent.OrderId);

            var paymentResult = await _ballanceService.TryWithdrawAsync(orderCreatedEvent.UserId, orderCreatedEvent.Amount, cancellationToken);

            var receipt = new Receipt
            {
                OrderId = orderCreatedEvent.OrderId,
                TotalCost = orderCreatedEvent.Amount,
                Status = paymentResult.IsSuccess ? PaymentStatus.Success : PaymentStatus.Error,
                CreatedAt = DateTime.UtcNow
            };

            await _receiptRepository.AddAsync(receipt, cancellationToken); //=====

            if (paymentResult.IsSuccess)
            {
                var paymentSuccessEvent = new PaymentSucceedEvent(orderCreatedEvent.OrderId, orderCreatedEvent.UserId, orderCreatedEvent.Amount, DateTime.UtcNow);

                await _eventPublisher.SendAsync(paymentSuccessEvent, cancellationToken);

                _logger.LogInformation("Payment succeeded for Order ID: {OrderId}", orderCreatedEvent.OrderId);
            }
            else
            {
                var paymentFailedEvent = new PaymentFailedEvent(orderCreatedEvent.UserId, orderCreatedEvent.OrderId, paymentResult.Reason);        

                await _eventPublisher.SendAsync(paymentFailedEvent, cancellationToken);

                _logger.LogInformation("Payment failed for Order ID: {OrderId} with reason: {Reason}", orderCreatedEvent.OrderId, paymentResult.Reason);
            }

        }
    }
}
