using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Mappers;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Repositories;

namespace IdentityService.API.Extentions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IAccessTokenService, AccessTokenSerivce>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            
        }
    }
}
