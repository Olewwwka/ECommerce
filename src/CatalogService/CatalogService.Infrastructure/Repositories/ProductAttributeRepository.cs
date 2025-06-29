using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductAttributeRepository(CatalogServiceDbContext context) : RepositoryBase<ProductAttribute>(context), IProductAttributeRepository
    {
    }
}
