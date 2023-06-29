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

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Supplies)
                .HasForeignKey(p => p.SupplierId);

            builder.Property(p => p.ReceivedBy)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
