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

            builder.Property(s => s.FullName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(17)
                .IsRequired(false);
            builder.Property(s => s.Balance)
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(s => s.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
