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

            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder.HasMany(s => s.OrderDetails)
                .WithOne(od => od.Sale)
                .HasForeignKey(od => od.SaleId);

            builder.Property(s => s.Receipt)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(s => s.Comment)
                .IsRequired(false)
                .HasMaxLength(500);
            builder.Property(s => s.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(s => s.TotalPaid)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(s => s.DiscountTotal)
                .HasColumnType("money")
                .HasDefaultValue(0);
            builder.Property(s => s.DiscountPercentage)
                .HasDefaultValue(0);
            builder.Property(s => s.SaleType)
                .HasDefaultValue(SaleType.Other)
                .IsRequired();
            builder.Property(s => s.Status)
                .HasDefaultValue(TransactionStatus.Finished);
        }
    }
}
