using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("Category_Id");

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
