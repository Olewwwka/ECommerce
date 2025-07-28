using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Data
{
    public class IdentityServiceDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ResetTokenEntity> ResetTokens { get; set; }
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityServiceDbContext).Assembly);
        }
    }
}
