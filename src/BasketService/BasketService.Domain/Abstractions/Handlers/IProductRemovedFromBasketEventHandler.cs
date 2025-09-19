using Ecommerce.Brokers.Events;

namespace BasketService.Domain.Abstractions.Handlers
{
    public interface IProductRemovedFromBasketEventHandler
    {
        Task HandleAsync(ProductRemovedFromBasketEvent productEvent, CancellationToken cancellationToken);
    }
}
