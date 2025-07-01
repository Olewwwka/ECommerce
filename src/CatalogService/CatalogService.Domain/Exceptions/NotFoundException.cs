using System.Security.Cryptography.X509Certificates;

namespace CatalogService.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}
