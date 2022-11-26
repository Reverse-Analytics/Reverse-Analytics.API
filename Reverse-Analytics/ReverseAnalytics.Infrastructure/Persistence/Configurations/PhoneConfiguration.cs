using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone");

            builder.HasKey(p => p.Id);

            builder.HasOne(ph => ph.Person)
                .WithMany(p => p.Phones)
                .HasForeignKey(ph => ph.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(13)
                .IsRequired();
            builder.Property(p => p.IsPrimary)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
