using Ecommerce.Brokers.Events;

namespace CatalogService.Domain.Abstractions.Handlers
{
    public interface IProductAddedToBasketEventHandler
    {
        Task HandleAsync(ProductAddedToBasketEvent productAddedToBasketEvent, CancellationToken cancellationToken);
    }
}
