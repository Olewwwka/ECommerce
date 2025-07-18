using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Entities
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalCost { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
