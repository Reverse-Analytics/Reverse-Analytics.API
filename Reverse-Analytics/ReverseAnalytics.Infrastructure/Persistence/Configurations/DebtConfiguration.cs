using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class DebtConfiguration : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.ToTable("Debt");

            builder.HasKey(d => d.Id);

            builder.HasOne(x => x.Transaction)
                .WithOne(d => d.Debt)
                .HasForeignKey<Debt>(d => d.TransactionId);

            builder.Property(d => d.TotalAmount)
                .HasColumnType("money")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(d => d.Remained)
                .HasColumnType("money")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(d => d.DueDate)
                .HasColumnType("date")
                .IsRequired(false);
            builder.Property(d => d.PaidDate)
                .HasColumnType("date")
                .IsRequired(false);
            builder.Property(e => e.Status)
                .HasDefaultValue(DebtStatus.PaymentRequired);
        }
    }
}
