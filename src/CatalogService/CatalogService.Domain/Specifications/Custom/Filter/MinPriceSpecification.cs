using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class MinPriceSpecification : Specification<Product>
    {
        private readonly decimal _minPrice;
        public MinPriceSpecification(decimal price)
        {
            _minPrice = price;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.Price >= _minPrice;
        }
    }
}
