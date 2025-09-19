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
    public class PaymentSucceedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventConsumer<OrderCreatedEvent> _consumer;
        private readonly ILogger<PaymentSucceedService> _logger;
        public PaymentSucceedService(IServiceScopeFactory scopeFactory, IEventConsumer<OrderCreatedEvent> consumer, ILogger<PaymentSucceedService> logger)
        {
            _logger = logger;
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                _logger.LogInformation("Received PaymentSucceedEvent with body: {Body}", Encoding.UTF8.GetString(args.Body.ToArray()));

                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var paymentEvent = JsonSerializer.Deserialize<PaymentSucceedEvent>(json);

                if (paymentEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IPaymentSucceedEventHandler>();

                    await handler.HandleAsync(paymentEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
