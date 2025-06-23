namespace IdentityService.DAL.Abstractions.Services
{
    public interface IDatabaseInitializer
    {
        Task InitializeRolesAsync(IServiceProvider serviceProvider);
    }
}
