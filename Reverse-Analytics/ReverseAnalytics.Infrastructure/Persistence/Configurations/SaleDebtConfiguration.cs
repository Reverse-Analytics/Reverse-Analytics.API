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

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Sale)
                .WithMany(s => s.SaleDebts)
                .HasForeignKey(x => x.SaleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
        }
    }
}
