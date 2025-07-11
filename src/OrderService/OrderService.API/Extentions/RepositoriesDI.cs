using OrderService.Domain.Abstractions;
using OrderService.Infrastructure.Repositories;

namespace OrderService.API.Extensions
{
    public static class RepositoriesDI
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderReposotory, OrderRepository>();
        }
    }
}
