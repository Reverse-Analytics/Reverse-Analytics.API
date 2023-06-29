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

            builder.Property(s => s.Receipt)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.Comments)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(x => x.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.TotalPaid)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.TotalDiscount)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.SaleType)
                .HasDefaultValue(SaleType.Other);

        }
    }
}
