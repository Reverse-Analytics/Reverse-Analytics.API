using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("Sale_Item");

            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Sale)
                .WithMany(o => o.SaleDetails)
                .HasForeignKey(sd => sd.SaleId);
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.ProductId);

            builder.Property(sd => sd.Quantity)
                .IsRequired();
            builder.Property(sd => sd.UnitPrice)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
