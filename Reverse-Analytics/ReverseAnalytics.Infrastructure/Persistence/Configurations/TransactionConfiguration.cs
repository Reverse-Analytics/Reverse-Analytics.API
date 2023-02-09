using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalDue)
                .HasColumnType("money")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(x => x.TotalPaid)
                .HasColumnType("money")
                .HasPrecision(2)
                .IsRequired();
            builder.Property(x => x.TransactionDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.Comments)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasDefaultValue(TransactionStatusType.Completed);
        }
    }
}
