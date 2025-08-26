using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using OrderService.Application.DTO;

namespace OrderService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class PaymentFailedTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public PaymentFailedTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task PaymentFailedEvent_Should_Publish_OrderCancelledEvent()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<OrderCancelledEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<OrderCancelledEvent>(_connectionFactory);

            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var ev = JsonSerializer.Deserialize<OrderCancelledEvent>(json);
                if (ev != null) taskSource.TrySetResult(ev);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var client = _factory._httpClient;
            var userId = Guid.NewGuid();
            var createDto = new CreateOrderDto
            {
                Items = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 10, Count = 1 }
                }
            };

            var response = await client.PostAsJsonAsync($"/orders/{userId}", createDto);
            var orderId = await response.Content.ReadFromJsonAsync<Guid>();

            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new PaymentFailedEvent(userId, orderId, ""));
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("OrderCreatedEvent", ExchangeType.Fanout, durable: true, autoDelete: false);

            // Act
            await channel.BasicPublishAsync("OrderCreatedEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            // Assert
            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            var evnt = await taskSource.Task;
            Assert.Equal(orderId, evnt.OrderId);
            Assert.Equal(userId, evnt.UserId);
        }
    }
}


