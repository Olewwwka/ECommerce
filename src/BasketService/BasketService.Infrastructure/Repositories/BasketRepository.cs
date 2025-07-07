using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace BasketService.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redis;
        public BasketRepository(IDatabase redis)
        {
            _redis = redis;
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _redis.KeyDeleteAsync(userId.ToString());
        }

        public async Task<Basket?> GetByUserIdAsync(Guid userId)
        {
            var basket = await _redis
                .StringGetAsync(userId.ToString());

            var data = JsonSerializer.Deserialize<Basket>(basket);

            return data;
        }

        public async Task<Basket> UpdateAsync(Basket basket)
        {
            var result = await _redis.StringSetAsync(
                basket.UserId.ToString(),
                JsonSerializer.Serialize(basket));

            return basket;
        }
    }
}
