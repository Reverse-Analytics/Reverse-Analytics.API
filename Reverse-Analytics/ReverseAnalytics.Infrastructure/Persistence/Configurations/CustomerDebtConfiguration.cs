using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class CustomerDebtConfiguration : IEntityTypeConfiguration<CustomerDebt>
    {
        public void Configure(EntityTypeBuilder<CustomerDebt> builder)
        {
            builder.HasKey(cd => cd.CustomerDebtId);

            builder.HasOne(cd => cd.Customer)
                .WithMany(c => c.CustomerDebts)
                .HasForeignKey(cd => cd.CustomerId)
                .HasConstraintName("Customer_FK");

            builder.Property(cd => cd.Amount)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}
