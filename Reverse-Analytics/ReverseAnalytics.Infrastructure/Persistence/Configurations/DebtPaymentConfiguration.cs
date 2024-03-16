using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class DebtPaymentConfiguration : IEntityTypeConfiguration<DebtPayment>
    {
        public void Configure(EntityTypeBuilder<DebtPayment> builder)
        {
            builder.ToTable(nameof(DebtPayment));
            builder.HasKey(dp => dp.Id);
        }
    }
}
