using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BLL.Abstractions
{
    public interface IEmailService
    {
        Task SendMessageAsync(string email, string token, CancellationToken cancellationToken);
    }
}
