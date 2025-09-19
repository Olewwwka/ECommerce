using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

namespace IdentityService.DAL.Services
{
    public class PutUpBallanceService : BackgroundService
    {
        private readonly IEventConsumer<PutUpBallanceEvent> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        public PutUpBallanceService(IEventConsumer<PutUpBallanceEvent> consumer,
            IServiceScopeFactory scopeFactory)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _consumer.AddListener(async (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var ballanceEvent = JsonSerializer.Deserialize<PutUpBallanceEvent>(json);

                if (ballanceEvent is not null)
                {
                    using var scope = _scopeFactory.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IPutUpUserBallanceHandler>();

                    await handler.HandleAsync(ballanceEvent, cancellationToken);
                }
            });

            return Task.CompletedTask;
        }
    }
}
