using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Abstractions
{
    public interface IOrderReposotory
    {
        Task<Guid> CreateAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetByUserId(Guid userId);
        Task<Guid> DeleteAsync(Guid id);
        Task<Guid> UpdateStatusAsync(Guid orderId, OrderStatus status);
    }
}
