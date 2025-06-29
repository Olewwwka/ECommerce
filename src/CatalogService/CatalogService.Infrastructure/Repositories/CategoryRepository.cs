using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Repositories
{
    public class CategoryRepository(CatalogServiceDbContext context) : RepositoryBase<Category>(context), ICategoryRepository
    {
    }
}
