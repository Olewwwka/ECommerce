namespace CatalogService.Domain.Exceptions
{
    public class CategoryMismatchException : Exception
    {
        public CategoryMismatchException(string message) : base(message)
        {

        }
    }
}
