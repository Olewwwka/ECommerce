using PaymentService.Domain.Enums;

namespace PaymentService.Application.DTO
{
    public record PaymentResponse
    {
        public PaymentStatus Status { get; set; }
        public Guid ReceiptId { get; set; }
    }
}
