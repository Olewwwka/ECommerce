using System.Text.Json;
using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task DeleteRefreshTokenAsync(string token)
        {
            await _refreshTokenRepository.DeleteTokenAsync(token);
        }

        public RefreshToken GenerateRefreshToken(Guid id)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = id,
                ExpiresAt = DateTime.UtcNow.AddHours(10)
            };
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
           return await _refreshTokenRepository.GetTokenAsync(token);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var token = JsonSerializer.Serialize(refreshToken);

            var expiry = refreshToken.ExpiresAt - refreshToken.CreatedAt;

            await _refreshTokenRepository.SetTokenAsync(refreshToken.Token, token, expiry);
        }

    }
}
