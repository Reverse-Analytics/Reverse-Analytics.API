using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierDebtConfiguration : IEntityTypeConfiguration<SupplierDebt>
    {
        public void Configure(EntityTypeBuilder<SupplierDebt> builder)
        {
            builder.HasKey(sd => sd.SupplierDebtId);

            builder.HasOne(sd => sd.Supplier)
                .WithMany(s => s.SupplierDebts)
                .HasForeignKey(sd => sd.SupplierId)
                .HasConstraintName("Supplier_FK");

            builder.Property(sd => sd.Amount)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}
