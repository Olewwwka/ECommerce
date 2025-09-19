using Ecommerce.Brokers.Events;

namespace IdentityService.DAL.Abstractions.Handlers
{
    public interface IPutUpUserBallanceHandler
    {
        Task HandleAsync(PutUpBallanceEvent ballanceEvent, CancellationToken cancellationToken);
    }
}
