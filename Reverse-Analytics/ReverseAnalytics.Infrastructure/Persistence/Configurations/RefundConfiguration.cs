using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable(nameof(Refund));

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Date)
                .IsRequired();
            builder.Property(r => r.ReceivedBy)
                .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
                .IsRequired();
            builder.Property(r => r.Reason)
                .HasMaxLength(ConfigurationConstants.DefaultStringMaxLength)
                .IsRequired();
            builder.Property(r => r.Comments)
                .HasMaxLength(ConfigurationConstants.LargeStringMaxLength)
                .IsRequired();
            builder.Property(r => r.SourceId)
                .IsRequired();
            builder.Property(r => r.TotalDue)
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(r => r.TotalPaid)
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(r => r.Source)
                .IsRequired();

            builder.Ignore(r => r.TransactionType);
            builder.Ignore(r => r.TransactionSource);
        }
    }
}
