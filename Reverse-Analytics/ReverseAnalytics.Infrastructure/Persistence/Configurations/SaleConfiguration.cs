using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations;

internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable(nameof(Sale));

        builder.HasOne(s => s.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CustomerId);
        builder.HasMany(s => s.SaleItems)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId);

        builder.Property(s => s.SaleDate)
            .IsRequired();
        builder.Property(s => s.SoldBy)
            .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
            .IsRequired(false);
        builder.Property(s => s.Comments)
            .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
            .IsRequired(false);
        builder.Property(s => s.TotalDue)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(s => s.TotalPaid)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(s => s.TotalDiscount)
            .HasPrecision(18, 2)
            .IsRequired();
        builder.Property(s => s.SaleType)
            .IsRequired();
        builder.Property(s => s.Status)
            .IsRequired();
        builder.Property(s => s.PaymentType)
            .IsRequired();
        builder.Property(s => s.Currency)
            .IsRequired();

        builder.Ignore(x => x.TransactionSource);
        builder.Ignore(x => x.TransactionType);
        builder.Ignore(x => x.RefundSource);
        builder.Ignore(x => x.RefundSourceId);
    }
}
