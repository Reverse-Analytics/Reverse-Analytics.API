using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

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
