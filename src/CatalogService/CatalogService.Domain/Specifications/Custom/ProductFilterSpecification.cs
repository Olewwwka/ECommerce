using CatalogService.Domain.Entities;
using CatalogService.Domain.Specifications.Custom.Filter;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom
{
    public class ProductFilterSpecification : Specification<Product>
    {
        private readonly ProductFilter _filter;
        public ProductFilterSpecification(ProductFilter filter)
        {
            _filter = filter;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            Specification<Product> specification = new TrueSpecification<Product>();

            if(_filter.CategoryId is not null)
            {
                specification = specification.And(new CategorySpecification(_filter.CategoryId.Value));
            }

            if(_filter.BrandId is not null)
            {
                specification = specification.And(new BrandSpecification(_filter.BrandId.Value));
            }

            if(_filter.SearchText is not null)
            {
                specification = specification.And(new SearchTextSpecification(_filter.SearchText));
            }

            if(_filter.MinPrice is not null)
            {
                specification = specification.And(new MinPriceSpecification(_filter.MinPrice.Value));
            }

            if(_filter.MaxPrice is not null)
            {
                specification = specification.And(new MaxPriceSpecification(_filter.MaxPrice.Value));
            }

            if(_filter.IsAvailable is not null)
            {
                specification = specification.And(new IsAvaliableSpecification(_filter.IsAvailable.Value));
            }

            return specification.ToExpression();
        }
    }
}
