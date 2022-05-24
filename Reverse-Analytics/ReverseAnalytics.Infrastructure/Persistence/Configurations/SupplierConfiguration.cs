using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");

            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Purchases)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            builder.Property(c => c.FirstName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(c => c.LastName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.CompanyName)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(c => c.Address)
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}
