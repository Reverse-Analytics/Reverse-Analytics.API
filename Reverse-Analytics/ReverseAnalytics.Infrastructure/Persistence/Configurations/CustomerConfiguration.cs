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

            builder.HasMany(c => c.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId);

            builder.Property(c => c.FullName)
                .HasMaxLength(250)
                .IsRequired(true);
            builder.Property(c => c.CompanyName)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(c => c.Discount)
                .HasDefaultValue(0);
            builder.Property(c => c.Balance)
                .HasDefaultValue(0);
            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
        }
    }
}
