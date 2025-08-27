using Ecommerce.Brokers.Events;

namespace PaymentService.Domain.Abstractions.Handlers
{
    public interface IOrderCreatedEventHandler
    {
        Task HandleAsync(OrderCreatedEvent orderCreatedEvent, CancellationToken cancellationToken);
    }
}
