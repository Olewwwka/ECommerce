using CatalogService.Domain.Constants;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Configurations
{
    public class ProductConfiguraton : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.MaxProductNameLenght);

            builder.Property(p => p.Description)
                .HasMaxLength(ValidationConstants.MaxProductDescriptionLenght);

            builder.Property(p => p.Price)
                .IsRequired();

            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.AttributeValues)
                .WithOne(av => av.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
