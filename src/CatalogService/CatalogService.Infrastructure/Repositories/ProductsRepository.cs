using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductsRepository(CatalogServiceDbContext context) : RepositoryBase<Product>(context), IProductRepository
    {
    }
}
