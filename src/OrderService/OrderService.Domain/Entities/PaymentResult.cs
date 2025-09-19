using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities
{
    public record PaymentResult(OrderStatus OrderStatus, string? Message)
    {
    }
}
