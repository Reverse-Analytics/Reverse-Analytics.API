using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplyDebtConfiguration : IEntityTypeConfiguration<SupplyDebt>
    {
        public void Configure(EntityTypeBuilder<SupplyDebt> builder)
        {
            builder.ToTable("Supply_Debt");

            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.TotalDue)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(sd => sd.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
        }
    }
}
