using CatalogService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace CatalogService.Infrastructure.Services
{
    public class ProductAddedToBasketService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventConsumer<ProductAddedToBasketEvent> _consumer;
        private readonly ILogger<ProductAddedToBasketService> _logger;

        public ProductAddedToBasketService(IServiceScopeFactory scopeFactory, IEventConsumer<ProductAddedToBasketEvent> consumer, ILogger<ProductAddedToBasketService> logger)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received OrderCreatedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));

                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var productEvent = JsonSerializer.Deserialize<ProductAddedToBasketEvent>(json);

                if (productEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IProductAddedToBasketEventHandler>();

                    await handler.HandleAsync(productEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
