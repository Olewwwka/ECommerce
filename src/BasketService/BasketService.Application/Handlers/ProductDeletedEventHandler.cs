using BasketService.Domain.Abstractions;
using BasketService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Events;

namespace BasketService.Application.Handlers
{
    public class ProductDeletedEventHandler : IProductDeletedEventHandler
    {
        private readonly IBasketRepository _basketRepository;
        public ProductDeletedEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<int> HandleAsync(ProductDeletedEvent productDeletedEvent, CancellationToken cancellationToken)
        {
            var baskets = await _basketRepository.GetAllBasketsAsync();

            int count = 0;

            foreach (var basket in baskets)
            {
                if (basket.Items.Any(item => item.ProductId == productDeletedEvent.ProductId))
                {
                    count++;
                    basket.Items.RemoveAll(item => item.ProductId == productDeletedEvent.ProductId);
                    await _basketRepository.UpdateAsync(basket);
                }
            }

            return count;
        }
    }
}
