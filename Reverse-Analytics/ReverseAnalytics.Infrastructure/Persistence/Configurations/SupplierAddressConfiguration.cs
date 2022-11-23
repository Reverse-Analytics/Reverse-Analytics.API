using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierAddressConfiguration : IEntityTypeConfiguration<SupplierAddress>
    {
        public void Configure(EntityTypeBuilder<SupplierAddress> builder)
        {
            builder.ToTable("Supplier_Address");

            builder.HasKey(sa => sa.Id);

            builder.HasOne(sa => sa.Supplier)
                .WithMany(s => s.Addresses)
                .HasForeignKey(sa => sa.SupplierId);
        }
    }
}
