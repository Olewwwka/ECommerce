using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();

            var userEntity = await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Email == email);

            return userEntity;
        }
    }
}
