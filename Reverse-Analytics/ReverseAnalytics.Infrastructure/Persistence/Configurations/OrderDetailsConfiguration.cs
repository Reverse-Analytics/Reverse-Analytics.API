using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("Order_Detail");

            builder.HasKey(od => od.Id);

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
            builder.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            builder.Property(od => od.Quantity)
                .IsRequired();
            builder.Property(od => od.UnitPrice)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(od => od.UnitPriceDiscount)
                .IsRequired(false);
        }
    }
}
