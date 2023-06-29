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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
        }
    }
}
