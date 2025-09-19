using Ecommerce.Brokers.Events;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Abstractions.Handlers
{
    public interface IPaymentSucceedEventHandler
    {
        Task<PaymentResult> HandleAsync(PaymentSucceedEvent paymentSucceedEvent, CancellationToken cancellationToken);
    }
}
