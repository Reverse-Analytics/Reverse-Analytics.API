using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            builder.Property(o => o.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(o => o.DiscountTotal)
                .HasColumnType("money")
                .IsRequired(false);
            builder.Property(o => o.DiscountPercentage)
                .IsRequired(false);
            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Unkown)
                .IsRequired(false);
            builder.Property(o => o.Comment)
                .IsRequired(false);
        }
    }
}
