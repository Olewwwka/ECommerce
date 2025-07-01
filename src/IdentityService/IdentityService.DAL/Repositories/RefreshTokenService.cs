using System.Text.Json;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using StackExchange.Redis;

namespace IdentityService.DAL.Repositories
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly IDatabase _redis;

        public RefreshTokenRepository(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task SetTokenAsync(string key, string value, TimeSpan expiry)
        {
            await _redis.StringSetAsync(key, value, expiry);
        }

        public async Task<RefreshToken?> GetTokenAsync(string token)
        {
            var refreshToken = await _redis.StringGetAsync(token);

            return JsonSerializer.Deserialize<RefreshToken>(refreshToken);
        }

        public async Task DeleteTokenAsync(string token)
        {
            await _redis.KeyDeleteAsync(token);
        }
    }
}
