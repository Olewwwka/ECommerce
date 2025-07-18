using PaymentService.Domain.Enums;

namespace PaymentService.Application.DTO
{
    public record ReceiptDTO
    {
        public Guid Id { get; set; } 
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public decimal TotalCost { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
