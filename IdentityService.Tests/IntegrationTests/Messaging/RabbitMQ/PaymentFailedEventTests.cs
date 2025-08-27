using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace IdentityService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class PaymentFailedEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public PaymentFailedEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task PaymentFailedEvent_Should_Be_Consumed()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<PaymentFailedEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<PaymentFailedEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var paymentEvent = JsonSerializer.Deserialize<PaymentFailedEvent>(json);
                if (paymentEvent != null) taskSource.TrySetResult(paymentEvent);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var userId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            // Act
            await using var conn = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await conn.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new PaymentFailedEvent(userId, orderId, "declined"));
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("PaymentFailedEvent", ExchangeType.Fanout, durable: true, autoDelete: false);
            await channel.BasicPublishAsync("PaymentFailedEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            // Assert
            Assert.True(taskSource.Task.IsCompleted, "PaymentFailedEvent was not received in time");
            var payEvent = await taskSource.Task;
            Assert.Equal(userId, payEvent.UserId);
            Assert.Equal(orderId, payEvent.OrderId);
        }
    }
}



