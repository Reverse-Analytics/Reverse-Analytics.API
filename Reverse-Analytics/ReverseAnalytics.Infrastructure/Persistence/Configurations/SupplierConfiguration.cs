using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");

            builder.HasKey(p => p.Id);

            builder.Property(s => s.FullName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.CompanyName)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.HasMany(s => s.Addresses)
                .WithOne(a => a.Supplier)
                .HasForeignKey(a => a.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(s => s.Supplies)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);
            builder.HasMany(s => s.SupplierDebts)
                .WithOne(sd => sd.Supplier)
                .HasForeignKey(sd => sd.SupplierId);
            builder.HasMany(s => s.SupplierPhones)
                .WithOne(sp => sp.Supplier)
                .HasForeignKey(sp => sp.SupplierId);
        }
    }
}
