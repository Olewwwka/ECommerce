using PaymentService.Application.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PaymentService.Tests.IntegrationTests.Database.PaymentService
{
    public class PutUpUserBallanceTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public PutUpUserBallanceTest(CustomWebApplicationFactory factory)
        {
            _client = factory._httpClient;
        }

        //[Fact]
        public async Task PutUpUserBallance_ShouldReturnReceiptDTO()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestBody = new { amount = (decimal)123 };

            // Act
            var response = await _client.PostAsJsonAsync($"/payments/ballance/{userId}", requestBody);

            // Assert
            var receipt = await response.Content.ReadFromJsonAsync<ReceiptDTO>();

            Assert.NotNull(receipt);
            Assert.Equal(requestBody.amount, receipt.TotalCost);
        }
    }
}
