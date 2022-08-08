using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(a => a.AddressId);

            builder.HasOne(a => a.City)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CityId)
                .HasConstraintName("City_FK");

            builder.HasMany(a => a.CustomerAddresses)
                .WithOne(ca => ca.Address);

            builder.Property(a => a.AddressDetails).IsRequired(false);
        }
    }
}
