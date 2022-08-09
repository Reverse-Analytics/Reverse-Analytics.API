using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.CustomerAddresses)
                .WithOne(a => a.City);

            builder.Property(c => c.CityName)
                .IsRequired()
                .HasMaxLength(250)
                .HasDefaultValue("Tashkent");
        }
    }
}
