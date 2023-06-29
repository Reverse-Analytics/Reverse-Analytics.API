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

            builder.Property(x => x.FullName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(13)
                .IsRequired(false);
            builder.Property(x => x.Balance)
                .HasDefaultValue(0)
                .HasPrecision(2)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
