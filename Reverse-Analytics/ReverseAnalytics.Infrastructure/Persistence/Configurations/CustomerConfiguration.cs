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

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FullName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.CompanyName)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.HasMany(c => c.Addresses)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.Customer)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Sales)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);
            builder.HasMany(c => c.CustomerPhones)
                .WithOne(cp => cp.Customer)
                .HasForeignKey(cp => cp.CustomerId);
            builder.HasMany(c => c.CustomerDebts)
                .WithOne(cd => cd.Customer)
                .HasForeignKey(cd => cd.CustomerId);
        }
    }
}
