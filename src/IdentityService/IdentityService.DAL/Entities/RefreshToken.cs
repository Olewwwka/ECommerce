using System;
using System.Collections.Generic;
using System.Linq;
namespace IdentityService.DAL.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => ExpiresAt < DateTime.UtcNow;
    }
}
