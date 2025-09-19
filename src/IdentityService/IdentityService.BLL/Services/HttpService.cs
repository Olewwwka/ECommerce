using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic to get information about receipt from PaymentService
    /// </summary>
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _logger;
        public HttpService(HttpClient client, ILogger<HttpService> logger)
        {
            _httpClient = client;
            _logger = logger;
        }
        /// <summary>
        /// Withdraw money from user ballance.
        /// </summary>
        /// <param name="orderId">Unique ID of order.</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation.</param>
        /// <returns>Receipt from PaymentService.</returns>
        /// <exception cref="Exception">If failed to withdraw money.</exception>
        public async Task<Receipt> TryGetReceiptAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();

            try
            {
                response = await _httpClient.GetAsync($"http://localhost:5005/receipts/order/{orderId}", cancellationToken);
                _logger.LogInformation("Get receipt for {orderId} failed", orderId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to withdraw balance: {ex.Message}", ex);
            }

            var data = await response.Content.ReadFromJsonAsync<Receipt>();

            _logger.LogInformation("Received response for Withdraw request");

            if (data is null)
            {
                _logger.LogInformation("Get receipt for {orderId} failed", orderId);
                return null;
            }
            else
            {
                return new Receipt
                {
                    Id = data.Id,
                    OrderId = data.OrderId,
                    TotalCost = data.TotalCost,
                    CreatedAt = data.CreatedAt
                };
            }
        }
    }
}
