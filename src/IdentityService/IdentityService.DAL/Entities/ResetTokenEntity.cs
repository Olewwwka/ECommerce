namespace IdentityService.DAL.Entities
{
    public class ResetTokenEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;

        public DateTime ExspiresAt { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired => ExspiresAt < DateTime.UtcNow;
    }
}
