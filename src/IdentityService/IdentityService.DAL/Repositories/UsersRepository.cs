using System.Threading.Tasks;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Data;
using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories
{
    public class UsersRepository : RepositoryBase<UserEntity> , IUsersRepository
    {
        public UsersRepository(IdentityServiceDbContext dbContext) : base(dbContext) { } 

        public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var userEntity = await _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(roles => roles.Role)
                .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

            return userEntity;
        }
    }
}
