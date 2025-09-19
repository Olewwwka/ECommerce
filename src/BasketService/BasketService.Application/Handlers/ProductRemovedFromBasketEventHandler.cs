using BasketService.Domain.Abstractions;
using BasketService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.Logging;

namespace BasketService.Application.Handlers
{
    public class ProductRemovedFromBasketEventHandler : IProductRemovedFromBasketEventHandler
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<ProductRemovedFromBasketEventHandler> _logger;
        public ProductRemovedFromBasketEventHandler(IBasketRepository basketRepository, ILogger<ProductRemovedFromBasketEventHandler> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }
        public async Task HandleAsync(ProductRemovedFromBasketEvent productEvent, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetByUserIdAsync(productEvent.UserId);

            if (basket is null)
            {
                _logger.LogWarning("Basket not found for UserId: {UserId}", productEvent.UserId);
                return;
            }

            basket.RemoveItem(productEvent.ProductId, productEvent.Quantity);

            await _basketRepository.UpdateAsync(basket);

            _logger.LogInformation(
                "Removed {Quantity} of product {ProductId} from basket for UserId {UserId}",
                productEvent.Quantity, productEvent.ProductId, productEvent.UserId);
        }
    }
}
