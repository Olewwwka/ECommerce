namespace OrderService.Domain.Exceptions
{
    public class UserHasUnpaidOrderException : Exception
    {
        public UserHasUnpaidOrderException(string message) : base(message)
        {
            
        }
    }
}
