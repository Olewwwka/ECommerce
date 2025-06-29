using CatalogService.Domain.Constants;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Configurations
{
    public class BrandConfiguratuin : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.MaxBrandNameLenght);

            builder.Property(b => b.LogoUrl)
                .IsRequired()
                .HasMaxLength(ValidationConstants.MaxBrandURLLenght);
        }
    }
}
