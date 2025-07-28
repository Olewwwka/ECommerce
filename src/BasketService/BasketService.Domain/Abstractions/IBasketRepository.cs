using BasketService.Domain.Entities;

namespace BasketService.Domain.Abstractions
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByUserIdAsync(Guid userId);
        Task<Basket> UpdateAsync(Basket basket);
        Task DeleteAsync(Guid userId);
    }
}
