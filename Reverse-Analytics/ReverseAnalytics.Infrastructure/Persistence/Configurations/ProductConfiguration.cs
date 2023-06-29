using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.Property(p => p.ProductCode)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.ProductName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasMaxLength(5000)
                .IsRequired(false);
            builder.Property(p => p.Volume)
                .HasPrecision(2);
            builder.Property(p => p.Weight)
                .HasPrecision(2);
            builder.Property(p => p.SupplyPrice)
                .HasColumnType("money");
            builder.Property(p => p.SalePrice)
                .HasColumnType("money");
        }
    }
}
