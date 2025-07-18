namespace PaymentService.Application.DTO
{
    public record PaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal TotalCost { get; set; }
    }
}
