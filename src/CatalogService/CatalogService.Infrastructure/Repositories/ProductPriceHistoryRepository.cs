using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductPriceHistoryRepository(CatalogServiceDbContext context) : RepositoryBase<ProductPriceHistory>(context)
    {
    }
}
