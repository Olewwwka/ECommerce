using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace IdentityService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class PutUpBallanceEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public PutUpBallanceEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task PutUpBallanceEvent_Should_Be_Consumed()
        {
            // Arrange
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

            var userId = Guid.NewGuid();
            var amount = 25m;

            // Act
            await using var conn = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await conn.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new PutUpBallanceEvent(userId, amount));
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("PutUpBallanceEvent", ExchangeType.Fanout, durable: true, autoDelete: false);
            await channel.BasicPublishAsync("PutUpBallanceEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            // Assert
            var @event = await taskSource.Task;
            Assert.Equal(userId, @event.UserId);
            Assert.Equal(amount, @event.Amount);
        }
    }
}



