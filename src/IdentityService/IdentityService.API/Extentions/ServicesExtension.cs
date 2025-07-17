using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions.Services;
using IdentityService.DAL.Data;

namespace IdentityService.API.Extentions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        }
    }
}
