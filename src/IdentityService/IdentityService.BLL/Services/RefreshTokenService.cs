using System.Text.Json;
using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic of refresh tokens
    /// Contains such methods as delete, generate get and save refresh tokens
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        /// <summary>
        /// Method to delete refresh token from database
        /// </summary>
        /// <param name="token">Key of token</param>
        public async Task DeleteRefreshTokenAsync(string token)
        {
            await _refreshTokenRepository.DeleteTokenAsync(token);
        }
        /// <summary>
        /// Method to generate refresh token to user
        /// </summary>
        /// <param name="id">Unique id of user</param>
        /// <returns>RefreshToken</returns>
        public RefreshToken GenerateRefreshToken(Guid id)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = id,
                ExpiresAt = DateTime.UtcNow.AddHours(10)
            };
        }
        /// <summary>
        /// Method to get refresh token of user from database
        /// </summary>
        /// <param name="token">Key of token</param>
        /// <returns>RefreshToken</returns>
        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
           return await _refreshTokenRepository.GetTokenAsync(token);
        }
        /// <summary>
        /// Method to save refresh token of user in database
        /// </summary>
        /// <param name="refreshToken">Refresh token of user</param>
        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var token = JsonSerializer.Serialize(refreshToken);

            var expiry = refreshToken.ExpiresAt - refreshToken.CreatedAt;

            await _refreshTokenRepository.SetTokenAsync(refreshToken.Token, token, expiry);
        }

    }
}
