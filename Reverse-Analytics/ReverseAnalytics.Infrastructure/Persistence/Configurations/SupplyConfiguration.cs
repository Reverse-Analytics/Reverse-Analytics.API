using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplyConfiguration : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable("Supply");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Supplies)
                .HasForeignKey(p => p.SupplierId);

            builder.HasMany(p => p.SupplyDetails)
                .WithOne(pd => pd.Supply)
                .HasForeignKey(pd => pd.SupplyId);

            builder.Property(p => p.ReceivedBy)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(p => p.Comment)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(p => p.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(p => p.TotalPaid)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(p => p.Status)
                .HasDefaultValue(TransactionStatus.Finished);
        }
    }
}
