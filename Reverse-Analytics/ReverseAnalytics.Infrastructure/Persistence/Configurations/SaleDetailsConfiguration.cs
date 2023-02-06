using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SaleDetailsConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.ToTable("Sale_Detail");

            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Sale)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(sd => sd.SaleId);
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.ProductId);

            builder.Property(sd => sd.Quantity)
                .IsRequired();
            builder.Property(sd => sd.UnitPrice)
                .HasColumnType("money")
                .HasPrecision(2)
                .IsRequired();
        }
    }
}
