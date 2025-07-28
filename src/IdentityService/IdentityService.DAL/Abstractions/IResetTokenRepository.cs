using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.DAL.Abstractions
{
    public interface IResetTokenRepository : IRepositoryBase<ResetTokenEntity>
    {
        Task<ResetTokenEntity> GetResetTokenByTokenAsync(string token, CancellationToken cancellationToken);
    }
}
