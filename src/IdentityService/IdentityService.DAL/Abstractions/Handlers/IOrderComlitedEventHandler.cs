using Ecommerce.Brokers.Events;

namespace IdentityService.DAL.Abstractions.Handlers
{
    public interface IOrderComlitedEventHandler
    {
        Task HandleAsync(OrderCompletedEvent orderEvent, CancellationToken cancellationToken);
    }
}
