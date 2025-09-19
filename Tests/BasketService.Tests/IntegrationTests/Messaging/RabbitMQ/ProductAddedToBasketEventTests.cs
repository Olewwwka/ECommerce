using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace OrderService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class ProductAddedToBasketEventTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ConnectionFactory _connectionFactory;

        public ProductAddedToBasketEventTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _connectionFactory = factory.Services.GetRequiredService<ConnectionFactory>();
        }

        [Fact]
        public async Task AddItem_Should_Publish_ProductAddedToBasketEvent()
        {
            // Arrange
            var taskSource = new TaskCompletionSource<ProductAddedToBasketEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<ProductAddedToBasketEvent>(_connectionFactory);
            consumer.AddListener(async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var ev = JsonSerializer.Deserialize<ProductAddedToBasketEvent>(json);
                if (ev != null) taskSource.TrySetResult(ev);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var client = _factory.CreateClient();
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;

            // Act
            var response = await client.PostAsync($"/baskets/{userId}/items?productId={productId}&quantity={quantity}", content: null);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(20)));

            // Assert

            var productEvent = await taskSource.Task;
            Assert.Equal(userId, productEvent.UserId);
            Assert.Equal(productId, productEvent.ProductId);
            Assert.Equal(quantity, productEvent.Quantity);
        }
    }
}


