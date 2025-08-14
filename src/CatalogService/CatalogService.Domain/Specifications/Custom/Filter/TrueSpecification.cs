using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class TrueSpecification<T> : Specification<T> where T : class
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}
