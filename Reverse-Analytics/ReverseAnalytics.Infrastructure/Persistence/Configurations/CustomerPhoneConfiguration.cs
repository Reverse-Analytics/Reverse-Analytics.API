using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class CustomerPhoneConfiguration : IEntityTypeConfiguration<CustomerPhone>
    {
        public void Configure(EntityTypeBuilder<CustomerPhone> builder)
        {
            builder.ToTable("Customer_Phone");

            builder.HasKey(cp => cp.Id);

            builder.HasOne(cp => cp.Customer)
                .WithMany(c => c.CustomerPhones)
                .HasForeignKey(cp => cp.CustomerId);

            builder.Property(cp => cp.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);
            builder.Property(cp => cp.IsPrimary)
                .HasDefaultValue(false);
        }
    }
}
