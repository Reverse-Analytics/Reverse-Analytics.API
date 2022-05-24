using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SupplierId)
                .HasConstraintName("Supplier_FK");

            builder.HasMany(p => p.PurchaseDetails)
                .WithOne(pd => pd.Purchase)
                .HasForeignKey(pd => pd.PurchaseId);

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
