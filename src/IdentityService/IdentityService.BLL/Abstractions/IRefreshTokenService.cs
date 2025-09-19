using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service forworking with refresh tokens.
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Generate new refresh token for user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>New refresh token.</returns>
        RefreshToken GenerateRefreshToken(Guid id);

        /// <summary>
        /// Save refresh token in database.
        /// </summary>
        /// <param name="refreshToken">Refresh token to save.</param>
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Get refresh token by token string.
        /// </summary>
        /// <param name="token">Token string.</param>
        /// <returns>RefreshToken</returns>
        Task<RefreshToken?> GetRefreshTokenAsync(string token);

        /// <summary>
        /// Delete refresh token from database.
        /// </summary>
        /// <param name="token">Token string.</param>
        Task DeleteRefreshTokenAsync(string token);
    }
}
