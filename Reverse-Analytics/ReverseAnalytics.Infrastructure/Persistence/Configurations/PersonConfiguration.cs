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

            builder.HasMany(p => p.Debts)
                .WithOne(d => d.Person)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(p => p.FullName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.CompanyName)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(p => p.Address)
                .HasMaxLength(250)
                .IsRequired(false);
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(p => p.Balance)
                .HasColumnType("money")
                .HasDefaultValue(0m);
        }
    }
}
