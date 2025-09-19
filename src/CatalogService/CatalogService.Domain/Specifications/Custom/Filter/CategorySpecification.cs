using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class CategorySpecification : Specification<Product>
    {
        private readonly Guid _categoryId;

        public CategorySpecification(Guid categoryId)
        {
            _categoryId = categoryId;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.CategoryId == _categoryId;
        }
    }
}
