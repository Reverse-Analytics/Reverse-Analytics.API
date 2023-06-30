using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(c => c.Address)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(17)
                .IsRequired(false);
            builder.Property(c => c.Balance)
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(c => c.Discount)
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(c => c.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
