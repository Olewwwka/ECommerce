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
    public class PaymentFailedService : BackgroundService
    {
        private readonly IEventConsumer<PaymentFailedEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PaymentFailedService> _logger;
        public PaymentFailedService(IEventConsumer<PaymentFailedEvent> consumer,
            IServiceScopeFactory scopeFactory,
            ILogger<PaymentFailedService> logger)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
            _logger = logger;
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
