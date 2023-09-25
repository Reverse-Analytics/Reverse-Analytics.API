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

            builder.HasOne(s => s.SaleDebt)
                .WithOne(sd => sd.Sale)
                .HasForeignKey<SaleDebt>(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(s => s.Receipt)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(s => s.Comments)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(s => s.SaleType)
                .HasDefaultValue(SaleType.Other);

        }
    }
}
