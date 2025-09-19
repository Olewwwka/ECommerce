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
    public class OrderComplitedService : BackgroundService
    {
        private readonly IEventConsumer<OrderCompletedEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderComplitedService> _logger;
        public OrderComplitedService(IEventConsumer<OrderCompletedEvent> consumer, IServiceScopeFactory scopeFactory, ILogger<OrderComplitedService> logger)
        {
            _logger = logger;
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received OrderCreatedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var orderEvent = JsonSerializer.Deserialize<OrderCompletedEvent>(json);

                if (orderEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IOrderComplitedEventHandler>();

                    await handler.HandleAsync(orderEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
