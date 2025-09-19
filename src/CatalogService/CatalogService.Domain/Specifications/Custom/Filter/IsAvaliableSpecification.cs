using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class IsAvaliableSpecification : Specification<Product>
    {
        private readonly bool _isAvaliable;
        public IsAvaliableSpecification(bool isAvaliable)
        {
            _isAvaliable = isAvaliable;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.IsAvaliable == _isAvaliable;
        }
    }
}
