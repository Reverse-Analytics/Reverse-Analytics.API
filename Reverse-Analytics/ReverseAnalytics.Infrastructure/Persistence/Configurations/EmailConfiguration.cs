using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Email");

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ContactDetail)
                .WithMany(cd => cd.Emails)
                .HasForeignKey(e => e.ContactDetailId)
                .HasConstraintName("Contact_Detail_FK");

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(e => e.IsPrimary)
                .IsRequired(false);
        }
    }
}
