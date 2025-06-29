using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductImageRepository(CatalogServiceDbContext context) : RepositoryBase<ProductImage>(context), IProductImageRepository
    {
    }
}
