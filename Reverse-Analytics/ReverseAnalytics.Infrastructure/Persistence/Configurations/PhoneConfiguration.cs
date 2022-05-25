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

            builder.HasOne(p => p.ContactDetail)
                .WithMany(cd => cd.Phones)
                .HasForeignKey(p => p.ContactDetailId)
                .HasConstraintName("Contact_Detail_FK");

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(13)
                .IsRequired();
            builder.Property(p => p.IsPrimary)
                .IsRequired(false);
        }
    }
}
