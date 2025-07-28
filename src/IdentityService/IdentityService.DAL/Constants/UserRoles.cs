using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DAL.Constants
{
    public static class UserRoles
    {
        public static string Admin = "Admin";
        public static string User = "User";
        public static readonly string[] AllRoles = { Admin, User };
    }
}
