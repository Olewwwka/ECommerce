namespace PaymentService.Application.DTO
{
    public record PutUpUserBallanceDTO
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
