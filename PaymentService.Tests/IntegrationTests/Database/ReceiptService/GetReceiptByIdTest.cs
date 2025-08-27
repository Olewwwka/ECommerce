using PaymentService.Application.DTO;
using System.Net.Http.Json;

namespace PaymentService.Tests.IntegrationTests.Database.ReceiptService
{
    public class GetReceiptByIdTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetReceiptByIdTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        //[Fact]
        public async Task GetById_ShouldReturnReceiptDTO()
        {
            // Arrange
            var receiptId = Guid.NewGuid();

            var receipt = new ReceiptDTO
            {
                Id = receiptId,
                OrderId = Guid.NewGuid(),
                TotalCost = 500
            };

            await _client.PostAsJsonAsync("/payments/setup/receipt", receipt); 

            // Act
            var response = await _client.GetAsync($"/receipts/{receiptId}");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<ReceiptDTO>();

            Assert.NotNull(result);
            Assert.Equal(receiptId, result.Id);
        }
    }
}
