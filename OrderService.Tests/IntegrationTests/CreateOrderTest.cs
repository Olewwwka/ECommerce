using OrderService.Application.DTO;
using System.Net;
using System.Net.Http.Json;

namespace OrderService.Tests.IntegrationTests
{
    public class CreateOrderTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public CreateOrderTest(CustomWebApplicationFactory factory)
        {
            _client = factory._httpClient;
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnOrderId()
        {
            // Arrange
            var request = new CreateOrderDto()
            {
                Items = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 123, Count = 1 }
                }
            };

            var userId = Guid.NewGuid();

            // Act
            var response = await _client.PostAsJsonAsync($"/orders/{userId}", request);
            var orderId = await response.Content.ReadFromJsonAsync<Guid>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(orderId);
        }
    }
}
