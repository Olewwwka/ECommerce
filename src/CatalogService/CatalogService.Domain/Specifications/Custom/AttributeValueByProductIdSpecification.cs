using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Infrastructure
{
    public class AttributeValueByProductIdSpecification : Specification<ProductAttributeValue>
    {
        private readonly Guid _productId;
        private readonly Guid _attributeId;
        public AttributeValueByProductIdSpecification(Guid productId, Guid attributeId)
        {
            _productId = productId;
            _attributeId = attributeId;
        }
        public override Expression<Func<ProductAttributeValue, bool>> ToExpression()
        {
            return av => av.ProductId == _productId && av.ProductAttributeId == _attributeId;
        }
    }
}
