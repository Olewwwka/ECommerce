using Microsoft.Extensions.Logging;
using PaymentService.Application.Abstractions;
using PaymentService.Application.DTO;
using System.Net.Http.Json;

namespace PaymentService.Application.Services
{
    public class WithdrawBallanceService : IWithdrawBallanceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WithdrawBallanceService> _logger;
        public WithdrawBallanceService(HttpClient client, ILogger<WithdrawBallanceService> logger)
        {
            _httpClient = client;
            _logger = logger;
        }
        public async Task<WithdrawResult> TryWithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
        {


            var response = new HttpResponseMessage();

            try
            {
                 response = await _httpClient.PostAsync($"http://localhost:5001/users/{userId}/ballance/withdraw?amount={amount}", null, cancellationToken);
                _logger.LogInformation("Withdraw request sent for User ID: {UserId} with Amount: {Amount}", userId, amount);
            }
            catch (Exception ex)
            {
                return new WithdrawResult(false, ex.Message);
            }
            
            var data = await response.Content.ReadFromJsonAsync<WithdrawResult>();

            _logger.LogInformation("Received response for Withdraw request: {IsSuccess}, Reason: {Reason}", data?.IsSuccess, data?.Reason);

            if (data is null)
            {
                return new WithdrawResult(false, "Failed to withdraw balance. Please try again later.");
            }

            return new WithdrawResult(data.IsSuccess, data.Reason);
        }
    }
}
