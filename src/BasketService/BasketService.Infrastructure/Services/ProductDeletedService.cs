using BasketService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

namespace BasketService.Infrastructure.Services
{
    public class ProductDeletedService : BackgroundService
    {
        private readonly IEventConsumer<ProductDeletedEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        public ProductDeletedService(IEventConsumer<ProductDeletedEvent> consumer, IServiceScopeFactory scopeFactory)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }//product updated 
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var productDeletedEvent = JsonSerializer.Deserialize<ProductDeletedEvent>(json);

                if (productDeletedEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IProductDeletedEventHandler>();

                    await handler.HandleAsync(productDeletedEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
