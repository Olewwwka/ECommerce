using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Data
{
    public class IdentityServiceDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) : base(options) { }
    }
}
