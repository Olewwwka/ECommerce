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
    public class OrderComplitedEventService : BackgroundService
    {
        private readonly IEventConsumer<PaymentSucceedEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderCompletedEvent> _logger;
        public OrderComplitedEventService(IEventConsumer<PaymentSucceedEvent> consumer,
            IServiceScopeFactory scopeFactory,
            ILogger<OrderCompletedEvent> logger)
        {
            _logger = logger;
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received OrderCompletedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var orderEvent = JsonSerializer.Deserialize<OrderCompletedEvent>(json);

                if (orderEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IOrderComlitedEventHandler>();

                    await handler.HandleAsync(orderEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
