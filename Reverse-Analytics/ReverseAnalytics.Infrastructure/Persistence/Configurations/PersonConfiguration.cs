using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.LastName)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(p => p.CompanyName)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(p => p.Address)
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}
