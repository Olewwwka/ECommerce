using BasketService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace BasketService.Infrastructure.Services
{
    public class ProductRemovedFromBasketService : BackgroundService
    {
        private readonly IEventConsumer<ProductRemovedFromBasketEvent> _consumer;
        private readonly ILogger<ProductRemovedFromBasketService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public ProductRemovedFromBasketService(IEventConsumer<ProductRemovedFromBasketEvent> consumer, IServiceScopeFactory scopeFactory, ILogger<ProductRemovedFromBasketService> logger)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("ProductRemovedFromBasketEvent received: {Event}", args.Body);

                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var productDeletedEvent = JsonSerializer.Deserialize<ProductRemovedFromBasketEvent>(json);

                if (productDeletedEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IProductRemovedFromBasketEventHandler>();

                    await handler.HandleAsync(productDeletedEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
