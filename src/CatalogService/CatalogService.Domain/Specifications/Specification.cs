using CatalogService.Domain.Abstractions.Specification;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity) =>
            ToExpression().Compile().Invoke(entity);

        public Specification<T> And(Specification<T> spec) =>
            new AndSpecification<T>(this, spec);
    }
}
