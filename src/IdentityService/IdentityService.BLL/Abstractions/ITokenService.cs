using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserEntity entity);
    }
}
