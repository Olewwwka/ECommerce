using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Configurations
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(p => p.Product)
                   .WithMany(p => p.AttributeValues)
                   .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.Attribute)
                   .WithMany()
                   .HasForeignKey(p => p.AttributeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
