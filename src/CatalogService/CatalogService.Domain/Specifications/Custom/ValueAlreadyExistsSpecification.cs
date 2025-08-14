using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom
{
    public class ValueAlreadyExistsSpecification : Specification<ProductAttributeValue>
    {
        private readonly string _value;
        private readonly Guid _productAttribute;
        public ValueAlreadyExistsSpecification(string value, Guid productAttribute)
        {
            _value = value;
            _productAttribute = productAttribute;
        }
        public override Expression<Func<ProductAttributeValue, bool>> ToExpression()
        {
            return av => av.ProductAttributeId == _productAttribute && av.Value == _value;
        }
    }
}
