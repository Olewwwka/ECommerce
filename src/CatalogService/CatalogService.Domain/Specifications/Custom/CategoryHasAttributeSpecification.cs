using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Application
{
    public class CategoryHasAttributeSpecification : Specification<Category>
    {
        public Guid _attribyteId;
        public CategoryHasAttributeSpecification(Guid attribyteId)
        {
            _attribyteId = attribyteId;
        }
        public override Expression<Func<Category, bool>> ToExpression()
        {
            return category => category.Attributes.Any(a => a.Id == _attribyteId);
        }
    }
}
