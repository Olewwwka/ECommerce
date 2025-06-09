using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.DAL.Abstractions
{
    public interface IRefreshTokenRepository
    {
        Task SetTokenAsync(string key, string value, TimeSpan expiry);
        Task DeleteTokenAsync(string refreshToken);
        Task<RefreshToken?> GetTokenAsync(string refreshToken);
    }
}
