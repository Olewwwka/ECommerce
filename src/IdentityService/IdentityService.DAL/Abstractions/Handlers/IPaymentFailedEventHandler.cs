using Ecommerce.Brokers.Events;

namespace IdentityService.DAL.Abstractions.Handlers
{
    public interface IPaymentFailedEventHandler
    {
        Task HandleAsync(PaymentFailedEvent paymentFailedEvent, CancellationToken cancellationToken);
    }
}
