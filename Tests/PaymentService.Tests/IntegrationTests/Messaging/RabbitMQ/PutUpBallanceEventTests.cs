using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PaymentService.Application.DTO;
using PaymentService.Domain.Entities;
using RabbitMQ.Client;
using PaymentService.Application.Abstrations;

namespace PaymentService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class PutUpBallanceEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly IMongoDatabase _db;
        private readonly ConnectionFactory _connectionFactory;

        public PutUpBallanceEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _db = factory.Database;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact(Timeout = 30000)]
        public async Task PutUpUserBallance_Should_Publish_PutUpBallanceEvent_And_SaveReceipt()
        {
            var taskSource = new TaskCompletionSource<PutUpBallanceEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<PutUpBallanceEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var ev = JsonSerializer.Deserialize<PutUpBallanceEvent>(json);
                if (ev != null) taskSource.TrySetResult(ev);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var payService = scope.ServiceProvider.GetRequiredService<IPayService>();

            var dto = new PutUpUserBallanceDTO
            {
                UserId = Guid.NewGuid(),
                Amount = 50
            };

            var result = await payService.PutUpUserBallance(dto, CancellationToken.None);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            var @event = await taskSource.Task;
            Assert.Equal(dto.UserId, @event.UserId);
            Assert.Equal(dto.Amount, @event.Amount);

            var receipts = _db.GetCollection<Receipt>("receipts");
            var receipt = await receipts.Find(r => r.TotalCost == dto.Amount).FirstOrDefaultAsync();
            Assert.NotNull(receipt);
        }
    }
}


