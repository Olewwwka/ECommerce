using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Entities
{
    public class Receipt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalCost { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
