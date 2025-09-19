using IdentityService.BLL.Abstractions;
using IdentityService.BLL.EventHandlers;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions.Handlers;
using IdentityService.DAL.Abstractions.Services;
using IdentityService.DAL.Data;
using IdentityService.DAL.Services;

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
            services.AddScoped<IBallanceService, BallanceService>();
            services.AddHttpClient<IHttpService, HttpService>();

            services.AddScoped<IPutUpUserBallanceHandler, PutUpUserBallanceEventHandler>();
            services.AddScoped<IPaymentFailedEventHandler, PaymentFailedEventHandler>();
            services.AddScoped<IOrderComlitedEventHandler, OrderComlitedEventHandler>();
            services.AddScoped<IOrderFailedEventHandler, OrderFailedEventHandler>();

            services.AddHostedService<PutUpBallanceService>();
            //services.AddHostedService<PaymentFailedService>();
            services.AddHostedService<OrderComplitedEventService>();
            services.AddHostedService<OrderFailedService>();
        }
    }
}
