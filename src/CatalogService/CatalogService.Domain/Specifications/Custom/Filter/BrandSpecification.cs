using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class BrandSpecification : Specification<Product>
    {
        private readonly Guid _brandId;

        public BrandSpecification(Guid brandId)
        {
            _brandId = brandId;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.BrandId == _brandId;
        }
    }
}
