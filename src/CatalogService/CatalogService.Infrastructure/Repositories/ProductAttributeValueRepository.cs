using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductAttributeValueRepository(CatalogServiceDbContext context) : RepositoryBase<ProductAttributeValue>(context), IProductAttributeValueRepository
    {
    }
}
