using OrderService.Application.DTO;
using System.Net;
using System.Net.Http.Json;

namespace OrderService.Tests.IntegrationTests
{
    public class GetOrderByUserIdTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetOrderByUserIdTest(CustomWebApplicationFactory factory)
        {
            _client = factory._httpClient;
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrderDto()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var request = new CreateOrderDto()
            {
                Items = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 }
                }
            };

            var order = await _client.PostAsJsonAsync($"/orders/{userId}", request);
            var orderId = await order.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.GetAsync($"/orders/user/{userId}");
            var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(orders);
            Assert.Contains(orders, o => o.Id == orderId);
            Assert.All(orders, o => Assert.Equal(userId, o.UserId));
        }
    }
}
