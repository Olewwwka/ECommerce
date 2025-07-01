using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageUrl {  get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
        public ProductImage()
        {
            
        }
    }
}
