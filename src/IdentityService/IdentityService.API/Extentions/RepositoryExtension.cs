using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Repositories;

namespace IdentityService.API.Extentions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IResetTokenRepository, ResetTokenRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
        }
    }
}
