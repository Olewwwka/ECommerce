using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using BasketService.Domain.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace BasketService.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redis;
        private readonly TimeSpan _expiryHours;
        public BasketRepository(IConnectionMultiplexer redis, IOptions<RedisOptions> options)
        {
            _redis = redis.GetDatabase();
            _expiryHours = TimeSpan.FromHours(options.Value.ExpiryHours);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _redis.KeyDeleteAsync(userId.ToString());
        }

        public async Task<Basket?> GetByUserIdAsync(Guid userId)
        {
            var basket = await _redis
                .StringGetAsync(userId.ToString());

            if(basket.IsNullOrEmpty)
            {
                return null;
            }

            var data = JsonSerializer.Deserialize<Basket>(basket);

            return data;
        }

        public async Task<Basket> UpdateAsync(Basket basket)
        {
            var result = await _redis.StringSetAsync(
                basket.UserId.ToString(),
                JsonSerializer.Serialize(basket),
                _expiryHours);

            return basket;
        }
    }
}
