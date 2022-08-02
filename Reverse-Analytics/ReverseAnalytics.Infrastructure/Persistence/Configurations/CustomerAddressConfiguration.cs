using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.HasKey(ca => ca.CustomerAddressId);

            builder.HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAddresses)
                .HasForeignKey(ca => ca.CustomerId)
                .HasConstraintName("Customer_FK");
            builder.HasOne(ca => ca.Address)
                .WithMany(a => a.CustomerAddresses)
                .HasForeignKey(ca => ca.AddressId)
                .HasConstraintName("Address_FK");
        }
    }
}
