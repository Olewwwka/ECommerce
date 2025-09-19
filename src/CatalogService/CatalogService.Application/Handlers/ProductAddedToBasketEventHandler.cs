using CatalogService.Domain.Abstractions.Handlers;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.Logging;

namespace CatalogService.Application.Handlers
{
    public class ProductAddedToBasketEventHandler : IProductAddedToBasketEventHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductAddedToBasketEventHandler> _logger;
        private readonly IEventProducer _producer;
        public ProductAddedToBasketEventHandler(IProductRepository productRepository, ILogger<ProductAddedToBasketEventHandler> logger, IEventProducer producer)
        {
            _producer = producer;
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task HandleAsync(ProductAddedToBasketEvent productAddedToBasketEvent, CancellationToken cancellationToken)
        {
            var exisingProduct = await _productRepository.GetByIdAsync(productAddedToBasketEvent.ProductId, cancellationToken);

            if(exisingProduct is null)
            {
                throw new NotFoundException($"Product with ID {productAddedToBasketEvent.ProductId} not found.");
            }

            if(exisingProduct.Quantity <= productAddedToBasketEvent.Quantity)
            {
                _logger.LogWarning("Insufficient quantity for product {ProductId}. Available: {AvailableQuantity}, Requested: {RequestedQuantity}.", 
                    productAddedToBasketEvent.ProductId, exisingProduct.Quantity, productAddedToBasketEvent.Quantity);

                var productEvent = new ProductRemovedFromBasketEvent(productAddedToBasketEvent.UserId, productAddedToBasketEvent.ProductId, productAddedToBasketEvent.Quantity);

                await _producer.SendAsync(productEvent, cancellationToken);

                throw new InvalidOperationException($"Insufficient quantity for product {productAddedToBasketEvent.ProductId}. Available: {exisingProduct.Quantity}, Requested: {productAddedToBasketEvent.Quantity}.");
            }

            exisingProduct.Quantity -= productAddedToBasketEvent.Quantity;

            await _productRepository.UpdateAsync(exisingProduct, cancellationToken);

            _logger.LogInformation("Event productAddedToBasketEvent successfully done {ProductId}. Dec quantity: {Quantity}", productAddedToBasketEvent.ProductId, productAddedToBasketEvent.Quantity);
        }
    }
}
