using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.DAL.Abstractions
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task CreateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
