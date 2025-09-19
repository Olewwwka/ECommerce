using IdentityService.BLL.DTO;

namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for user balance operations.
    /// </summary>
    public interface IBallanceService
    {
        /// <summary>
        /// Withdraw money from user balance.
        /// </summary>
        /// <param name="userId">Unique ID of user</param>
        /// <param name="amount">Amount to withdraw.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>WithdrawResponse with withdraw status and money</returns>
        Task<WithdrawResponse> WithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken);
    }
}
