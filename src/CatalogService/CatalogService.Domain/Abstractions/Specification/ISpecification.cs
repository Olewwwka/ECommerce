using System.Linq.Expressions;

namespace CatalogService.Domain.Abstractions.Specification
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> ToExpression();
        bool IsSatisfiedBy(T entity) => ToExpression().Compile().Invoke(entity);
    }
}
