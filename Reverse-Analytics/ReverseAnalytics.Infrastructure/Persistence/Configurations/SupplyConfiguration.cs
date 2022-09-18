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

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Supplies)
                .HasForeignKey(p => p.SupplierId);

            builder.HasMany(p => p.SupplyDetails)
                .WithOne(pd => pd.Supply)
                .HasForeignKey(pd => pd.SupplyId);

            builder.Property(p => p.TotalDue)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(p => p.PaidAmount)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(p => p.Debt)
                .HasColumnType("money")
                .IsRequired(false);
            builder.Property(p => p.ReceivedBy)
                .IsRequired(false);
        }
    }
}
