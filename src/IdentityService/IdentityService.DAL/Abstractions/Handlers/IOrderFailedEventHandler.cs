using Ecommerce.Brokers.Events;

namespace IdentityService.DAL.Abstractions.Handlers
{
    public interface IOrderFailedEventHandler
    {
        Task HandleAsync(OrderFailedEvent orderFailedEvent, CancellationToken cancellationToken);
    }
}
