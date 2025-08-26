using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace IdentityService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class OrderCompletedEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public OrderCompletedEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task OrderCompletedEvent_Should_Be_Consumed()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<OrderCompletedEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<OrderCompletedEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var paymentEvent = JsonSerializer.Deserialize<OrderCompletedEvent>(json);
                if (paymentEvent != null) taskSource.TrySetResult(paymentEvent);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var orderId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            await using var conn = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await conn.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new OrderCompletedEvent { OrderId = orderId, UserId = userId, CompletedAt = DateTime.UtcNow });
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("OrderCompletedEvent", ExchangeType.Fanout, durable: true, autoDelete: false);
            await channel.BasicPublishAsync("OrderCompletedEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            // Act
            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            // Assert
            var payEvent = await taskSource.Task;
            Assert.Equal(orderId, payEvent.OrderId);
            Assert.Equal(userId, payEvent.UserId);
        }
    }
}



