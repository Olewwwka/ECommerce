namespace IdentityService.BLL.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey {  get; set; }
        public int ExpiryHours { get; set; }
    }
}
