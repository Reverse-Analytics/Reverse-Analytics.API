using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierDebtConfiguration : IEntityTypeConfiguration<SupplierDebt>
    {
        public void Configure(EntityTypeBuilder<SupplierDebt> builder)
        {
            builder.ToTable("Supplier_Debt");

            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Supplier)
                .WithMany(s => s.SupplierDebts)
                .HasForeignKey(sd => sd.SupplierId);

            builder.Property(sd => sd.Amount)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}
