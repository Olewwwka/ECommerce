using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class MaxPriceSpecification : Specification<Product>
    {
        private readonly decimal _maxPrice;
        public MaxPriceSpecification(decimal price)
        {
            _maxPrice = price;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.Price <= _maxPrice;
        }
    }
}
