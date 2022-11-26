using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierPhoneConfiguration : IEntityTypeConfiguration<SupplierPhone>
    {
        public void Configure(EntityTypeBuilder<SupplierPhone> builder)
        {
            builder.ToTable("Supplier_Phone");

            builder.HasOne(sp => sp.Supplier)
                .WithMany(s => s.SupplierPhones)
                .HasForeignKey(sp => sp.SupplierId);

            builder.Property(sp => sp.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);
            builder.Property(sp => sp.IsPrimary)
                .HasDefaultValue(false);
        }
    }
}
