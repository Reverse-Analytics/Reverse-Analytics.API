using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.CustomerAddress)
                .WithOne(ca => ca.Address)
                .HasForeignKey<CustomerAddress>(ca => ca.AddressId)
                .IsRequired(false);
            builder.HasOne(a => a.SupplierAddress)
                .WithOne(sa => sa.Address)
                .HasForeignKey<SupplierAddress>(sa => sa.AddressId);

            builder.Property(a => a.AddressDetails)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(a => a.AddressLandMark)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(a => a.Longtitude)
                .HasMaxLength(25)
                .IsRequired(false);
            builder.Property(a => a.Latitude)
                .HasMaxLength(25)
                .IsRequired(false);
            builder.Property(a => a.LocationOnMap)
                .HasMaxLength(150)
                .IsRequired(false);
        }
    }
}
