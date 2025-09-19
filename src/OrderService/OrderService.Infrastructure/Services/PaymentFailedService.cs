using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Abstractions.Handlers;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Services
{
    public class PaymentFailedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventConsumer<OrderCreatedEvent> _consumer;
        private readonly ILogger<PaymentFailedService> _logger;
        public PaymentFailedService(IServiceScopeFactory scopeFactory, IEventConsumer<OrderCreatedEvent> consumer, ILogger<PaymentFailedService> logger)
        {
            _logger = logger;
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received PaymentFailedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));

                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var paymentEvent = JsonSerializer.Deserialize<PaymentFailedEvent>(json);

                if (paymentEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IPaymentFailedEventHandler>();

                    await handler.HandleAsync(paymentEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
