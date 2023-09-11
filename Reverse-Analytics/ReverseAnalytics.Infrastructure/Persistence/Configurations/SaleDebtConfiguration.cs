using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SaleDebtConfiguration : IEntityTypeConfiguration<SaleDebt>
    {
        public void Configure(EntityTypeBuilder<SaleDebt> builder)
        {
            builder.ToTable("Sale_Debt");

            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.TotalDue)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(sd => sd.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
            builder.Property(sd => sd.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
        }
    }
}
