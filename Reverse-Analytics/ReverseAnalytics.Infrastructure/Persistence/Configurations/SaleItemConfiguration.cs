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
                .WithMany(o => o.SaleItems)
                .HasForeignKey(sd => sd.SaleId);
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.SaleItems)
                .HasForeignKey(sd => sd.ProductId);
        }
    }
}
