using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken(Guid id);
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task DeleteRefreshTokenAsync(string token);
    }
}
