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
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("Category_FK");

            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId);
            builder.HasMany(p => p.PurchaseDetails)
                .WithOne(pd => pd.Product)
                .HasForeignKey(pd => pd.ProductId);

            builder.Property(p => p.ProductName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.PurchasePrice)
                .HasColumnType("money");
            builder.Property(p => p.SalePrice)
                .HasColumnType("money");
        }
    }
}
