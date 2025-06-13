using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BLL.Constants
{
    public static class ValidationRules
    {
        public static string EmailErrorMessage = "Email must be in the form: example@gmail.com";

        public static string EmailNotEmptyMessage = "Email must not be empty";
        public static string NameNotEmptyMessage = "Name must not be empty";

        public static string PasswordMinLenghtMessage = "Password should be greater then 6 characters";
        public static string PasswordMaxLenghtMessage = "Password should be less then 20 characters";

        public static int PasswordMinLenght = 6;
        public static int PasswordMaxLenght = 20;

        public static string NameMinLenghtMessage = "Name should be greater then 2 characters";
        public static string NameMaxLenghtMessage = "Name should be less then 16 characters";

        public static int NameMinLenght = 6;
        public static int NameMaxLenght = 20;

    }
}
