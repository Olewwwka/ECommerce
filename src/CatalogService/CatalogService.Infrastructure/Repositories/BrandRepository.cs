using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class BrandRepository(CatalogServiceDbContext context) : RepositoryBase<Brand>(context), IBrandRepository
    {
    }
}
