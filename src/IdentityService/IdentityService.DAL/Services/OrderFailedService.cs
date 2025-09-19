using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace IdentityService.DAL.Services
{
    public class OrderFailedService : BackgroundService
    {
        private readonly IEventConsumer<OrderFailedEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderFailedService> _logger;
        public OrderFailedService(IEventConsumer<OrderFailedEvent> consumer,
            IServiceScopeFactory scopeFactory,
            ILogger<OrderFailedService> logger)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received OrderFailedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var orderEvent = JsonSerializer.Deserialize<OrderFailedEvent>(json);

                if (orderEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IOrderFailedEventHandler>();

                    await handler.HandleAsync(orderEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
