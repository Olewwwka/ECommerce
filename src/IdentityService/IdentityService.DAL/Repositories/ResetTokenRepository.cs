using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Data;
using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.Repositories
{
    public class ResetTokenRepository : RepositoryBase<ResetTokenEntity>, IResetTokenRepository
    {
        public ResetTokenRepository(IdentityServiceDbContext context) : base(context) { }

        public async Task<ResetTokenEntity> GetResetTokenByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.ResetTokens.FirstOrDefaultAsync(resetToken => resetToken.Token == token, cancellationToken);
        }
    }
}
