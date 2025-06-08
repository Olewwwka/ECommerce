using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(Guid id, string name, string email, string role);
    }
}
