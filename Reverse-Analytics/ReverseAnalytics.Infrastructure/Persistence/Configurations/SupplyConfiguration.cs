using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplyConfiguration : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable("Supply");

            builder.HasOne(s => s.Supplier)
                .WithMany(x => x.Supplies)
                .HasForeignKey(s => s.SupplierId);

            builder.Property(s => s.ReceivedBy)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(s => s.Comment)
                .HasMaxLength(2500)
                .IsRequired(false);
            builder.Property(x => x.TotalDue)
                .HasColumnType("money")
                .HasPrecision(18, 2);
            builder.Property(x => x.TotalPaid)
                .HasColumnType("money")
                .HasPrecision(18, 2);
        }
    }
}
