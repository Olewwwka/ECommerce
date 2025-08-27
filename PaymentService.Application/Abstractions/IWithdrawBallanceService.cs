using PaymentService.Application.DTO;

namespace PaymentService.Application.Abstractions
{
    public interface IWithdrawBallanceService
    {
        Task<WithdrawResult> TryWithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken);
    }
}
