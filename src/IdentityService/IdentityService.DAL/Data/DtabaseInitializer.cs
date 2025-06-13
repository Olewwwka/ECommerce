using IdentityService.DAL.Constants;
using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.DAL.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();

            foreach(var roleName in UserRoles.AllRoles)
            {
                var existingRole = await context.Roles.AnyAsync(role => role.Name == roleName);

                if(!existingRole)
                {
                    await context.Roles.AddAsync(new RoleEntity { Id = Guid.NewGuid(), Name = roleName });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
