using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Ecommerce.Brokers.Producers;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using OrderService.Application.DTO;

namespace OrderService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class PaymentSucceedTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public PaymentSucceedTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task PaymentSucceedEvent_Should_Publish_OrderCompletedEvent()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<OrderCompletedEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<OrderCompletedEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var ev = JsonSerializer.Deserialize<OrderCompletedEvent>(json);
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

            var createResp = await client.PostAsJsonAsync($"/orders/{userId}", createDto);
            var orderId = await createResp.Content.ReadFromJsonAsync<Guid>();

            await using var conn = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await conn.CreateChannelAsync();

            var json = JsonSerializer.Serialize(new PaymentSucceedEvent(orderId, userId, 50m, DateTime.UtcNow));
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties { ContentType = "application/json", Persistent = true };

            await channel.ExchangeDeclareAsync("OrderCreatedEvent", ExchangeType.Fanout, durable: true, autoDelete: false);

            // Act
            await channel.BasicPublishAsync("OrderCreatedEvent", routingKey: string.Empty, mandatory: true, basicProperties: properties, body: body);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));


            var @event = await taskSource.Task;
            Assert.Equal(orderId, @event.OrderId);
            Assert.Equal(userId, @event.UserId);
        }
    }
}


