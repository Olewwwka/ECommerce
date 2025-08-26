using PaymentService.Application.DTO;
using PaymentService.Domain.Enums;
using System.Net;
using System.Net.Http.Json;

namespace PaymentService.Tests.IntegrationTests.Database.PaymentService
{
    public class GetPaymentStatusByOrderIdTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetPaymentStatusByOrderIdTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        public async Task GetPaymentStatusByOrderId_ShouldReturnPaymentStatus_WhenOrderIdExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            var paymentRequest = new PaymentRequest
            {
                OrderId = orderId,
                TotalCost = 150
            };
            var postResponse = await _client.PostAsJsonAsync("/payments", paymentRequest);

            // Act
            var response = await _client.GetAsync($"/payments/status/{orderId}");

            // Assert
            var status = await response.Content.ReadFromJsonAsync<PaymentStatus>();
            Assert.Equal(PaymentStatus.Success, status);
        }
    }
}
