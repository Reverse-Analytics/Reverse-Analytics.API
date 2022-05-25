using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class ContactDetailsConfiguration : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder.ToTable("Contact_Detail");

            builder.HasKey(cd => cd.Id);

            builder.HasOne(cd => cd.Person)
                .WithOne(p => p.ContactDetails)
                .HasForeignKey<ContactDetail>(cd => cd.PersonId)
                .HasConstraintName("Person_FK");

            builder.HasMany(cd => cd.Emails)
                .WithOne(e => e.ContactDetail)
                .HasForeignKey(e => e.ContactDetailId);
            builder.HasMany(cd => cd.Phones)
                .WithOne(p => p.ContactDetail)
                .HasForeignKey(p => p.ContactDetailId);
        }
    }
}
