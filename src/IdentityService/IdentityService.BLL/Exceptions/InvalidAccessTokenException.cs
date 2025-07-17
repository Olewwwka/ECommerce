namespace IdentityService.BLL.Exceptions
{
    public class InvalidAccessTokenException : Exception
    {
        public InvalidAccessTokenException(string message) : base(message)
        {
            
        }
    }
}
