using CatalogService.Domain.Constants;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Configurations
{
    public class ProductAttribureConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.MaxProductAttributeNameLenght);

            builder.Property(p => p.AttributeType)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Attributes)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
