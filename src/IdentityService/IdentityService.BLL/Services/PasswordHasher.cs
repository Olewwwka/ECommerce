using IdentityService.BLL.Abstractions;

namespace IdentityService.BLL.Services
{  
   /// <summary>
   /// Serivce that contains logic to work with users password
   /// Contains such methods, as hash and verify users password
   /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Hashing password of user
        /// </summary>
        /// <param name="password">Password of user</param>
        /// <returns>hashed password</returns>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        /// <summary>
        /// Verifying password of user
        /// </summary>
        /// <param name="password">Password of user</param>
        /// <param name="passwordHash">Hashed password of user</param>
        /// <returns>Status of verifying</returns>
        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
