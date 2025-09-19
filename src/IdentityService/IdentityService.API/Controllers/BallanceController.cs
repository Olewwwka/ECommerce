using IdentityService.BLL.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    /// <summary>
    /// Controller that contains operations to working with user ballance.
    /// </summary>
    [ApiController]
    [Route("users/{userId:guid}/ballance")]
    public class BallanceController : ControllerBase
    {
        private readonly IBallanceService _ballanceService;

        public BallanceController(IBallanceService ballanceService)
        {
            _ballanceService = ballanceService;
        }

        /// <summary>
        /// Withdraw money from the user balance.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="amount">Amount to withdraw from user balance.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Return result of withdraw operation.</returns>
        /// <response code="200">Withdrawal successful and returns the updated balance.</response>
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
        {
            var result = await _ballanceService.WithdrawAsync(userId, amount, cancellationToken);
            return Ok(result);
        }
    }
}
