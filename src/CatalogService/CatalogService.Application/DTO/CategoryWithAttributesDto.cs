using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.DTO
{
    public class CategoryWithAttributesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductAttributeDto> Attributes { get; set; } = new();
    }
}
