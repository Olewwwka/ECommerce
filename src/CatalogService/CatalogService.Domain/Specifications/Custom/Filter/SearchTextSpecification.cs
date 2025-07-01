using CatalogService.Domain.Entities;
using System.Linq.Expressions;

namespace CatalogService.Domain.Specifications.Custom.Filter
{
    public class SearchTextSpecification : Specification<Product>
    {
        private readonly string _text;
        public SearchTextSpecification(string text)
        {
            _text = text;
        }
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return p => p.Name.ToLower().Contains(_text.ToLower()) || p.Description.ToLower().Contains(_text.ToLower());
        }
    }
}
