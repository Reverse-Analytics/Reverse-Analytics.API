using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class DebtConfiguration : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.ToTable("Debt");

            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Person)
                .WithMany(p => p.Debts)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.Amount)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(d => d.DebtDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(d => d.DueDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(d => d.PaidDate)
                .HasColumnType("date")
                .IsRequired(false);
        }
    }
}
