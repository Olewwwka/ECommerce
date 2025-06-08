using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.BLL.DTO;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<AuthResponse> RefreshAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
    }
}
