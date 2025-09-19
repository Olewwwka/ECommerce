using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace OrderService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class ProductRemovedFromBasketEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public ProductRemovedFromBasketEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task ProductRemovedFromBasketEvent_Should_Be_Consumed_By_BasketService()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<ProductRemovedFromBasketEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<ProductRemovedFromBasketEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var ev = JsonSerializer.Deserialize<ProductRemovedFromBasketEvent>(json);
                if (ev != null) taskSource.TrySetResult(ev);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            // Act
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new ProductRemovedFromBasketEvent(userId, productId, 1));
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("ProductRemovedFromBasketEvent", ExchangeType.Fanout, durable: true, autoDelete: false);
            await channel.BasicPublishAsync("ProductRemovedFromBasketEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            // Assert

            var productEvent = await taskSource.Task;
            Assert.Equal(userId, productEvent.UserId);
            Assert.Equal(productId, productEvent.ProductId);
        }
    }
}




