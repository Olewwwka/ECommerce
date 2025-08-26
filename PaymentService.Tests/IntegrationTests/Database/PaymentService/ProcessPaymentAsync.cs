using PaymentService.Application.DTO;
using PaymentService.Domain.Enums;
using System.Net;
using System.Net.Http.Json;

namespace PaymentService.Tests.IntegrationTests.Database.PaymentService
{
    public class ProcessPaymentAsync : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public ProcessPaymentAsync(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        public async Task ProcessPayment_ShouldReturnPaymentDto()
        {
            // Arrange
            var request = new PaymentRequest
            {
                OrderId = Guid.NewGuid(),
                TotalCost = 150
            };

            // Act
            var response = await _client.PostAsJsonAsync("/payments", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();

            Assert.Equal(request.OrderId, result.ReceiptId); 
        }
    }
}
