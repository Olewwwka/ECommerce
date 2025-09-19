using Ecommerce.Brokers.Events;

namespace BasketService.Domain.Abstractions.Handlers
{
    public interface IOrderComplitedEventHandler
    {
        Task HandleAsync(OrderCompletedEvent orderCompletedEvent, CancellationToken cancellationToken);
    }
}
