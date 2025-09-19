using Ecommerce.Brokers.Events;

namespace BasketService.Domain.Abstractions.Handlers
{
    public interface IProductDeletedEventHandler
    {
        public Task<int> HandleAsync(ProductDeletedEvent productDeletedEvent, CancellationToken cancellationToken);
    }
}
