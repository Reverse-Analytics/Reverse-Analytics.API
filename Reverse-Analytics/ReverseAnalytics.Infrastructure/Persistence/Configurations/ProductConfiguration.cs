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

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.Property(x => x.ProductCode)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.ProductName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(5000)
                .IsRequired(false);
            builder.Property(x => x.Volume)
                .HasPrecision(2);
            builder.Property(x => x.Weight)
                .HasPrecision(2);
            builder.Property(x => x.SupplyPrice)
                .HasColumnType("money");
            builder.Property(x => x.SalePrice)
                .HasColumnType("money");
        }
    }
}
