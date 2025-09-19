using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentService.Domain.Abstractions.Handlers;
using System.Text;
using System.Text.Json;

namespace PaymentService.Infrastructure.Services
{
    public class OrderCreatedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventConsumer<OrderCreatedEvent> _consumer;
        private readonly ILogger<OrderCreatedService> _logger;

        public OrderCreatedService(IServiceScopeFactory scopeFactory, IEventConsumer<OrderCreatedEvent> consumer, ILogger<OrderCreatedService> logger)
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
                var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                if (orderEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IOrderCreatedEventHandler>();

                    await handler.HandleAsync(orderEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
