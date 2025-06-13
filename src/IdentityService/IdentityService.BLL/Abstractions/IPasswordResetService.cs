using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BLL.Abstractions
{
    public interface IPasswordResetService
    {
        Task SendResetEmailMessageAsync(string email, CancellationToken cancellationToken);
        Task<bool> ResetPasswordAsync(string email, string token, string password, CancellationToken cancellationToken);
    }
}
