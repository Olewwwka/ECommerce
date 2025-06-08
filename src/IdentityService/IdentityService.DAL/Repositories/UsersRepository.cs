using System.Threading.Tasks;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Data;
using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IdentityServiceDbContext _context;
        public UsersRepository(IdentityServiceDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);

            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

            return userEntity;
        }
    }
}
