using PaymentService.Application.DTO;
using System.Net.Http.Json;

namespace PaymentService.Tests.IntegrationTests.Database.ReceiptService
{
    public class GetReceiptByOrderIdTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetReceiptByOrderIdTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        //[Fact]
        public async Task GetByOrderId_ShouldReturnReceiptDTO()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var receipt = new ReceiptDTO
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                TotalCost = 200
            };

            await _client.PostAsJsonAsync("/payments/setup/receipt", receipt);

            // Act
            var response = await _client.GetAsync($"/receipts/order/{orderId}");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<ReceiptDTO>();

            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
        }
    }
}
