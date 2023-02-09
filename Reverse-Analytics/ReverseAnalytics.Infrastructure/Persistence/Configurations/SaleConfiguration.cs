using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder.HasMany(s => s.OrderDetails)
                .WithOne(od => od.Sale)
                .HasForeignKey(od => od.SaleId);

            builder.Property(s => s.Receipt)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(s => s.Discount)
                .HasColumnType("money")
                .HasDefaultValue(0);
            builder.Property(s => s.SaleType)
                .HasDefaultValue(SaleType.Other)
                .IsRequired();
        }
    }
}
