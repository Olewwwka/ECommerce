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
    public class RolesRepository : RepositoryBase<RoleEntity>, IRolesRepository
    {
        public RolesRepository(IdentityServiceDbContext context) : base(context) { }

        public async Task<RoleEntity> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.Name == name, cancellationToken);
        }
    }
}
