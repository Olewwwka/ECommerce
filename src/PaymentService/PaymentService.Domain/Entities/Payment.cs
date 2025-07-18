namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
