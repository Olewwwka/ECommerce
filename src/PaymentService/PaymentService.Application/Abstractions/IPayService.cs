using PaymentService.Application.DTO;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.Abstrations
{
    public interface IPayService
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken);
        Task<PaymentStatus> GetPaymentStatusByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
