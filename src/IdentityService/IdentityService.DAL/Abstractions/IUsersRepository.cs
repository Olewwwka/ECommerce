using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.DAL.Abstractions
{
    public interface IUsersRepository : IRepositoryBase<UserEntity>
    {
        Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
