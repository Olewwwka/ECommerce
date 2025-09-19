using Ecommerce.Brokers.Events;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Abstractions.Handlers
{
    public interface IPaymentFailedEventHandler
    {
        Task<PaymentResult> HandleAsync(PaymentFailedEvent paymentFailedEvent, CancellationToken cancellationToken);
    }
}
