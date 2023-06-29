using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable(nameof(Refund));

            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Sale)
                .WithMany(s => s.Refunds)
                .HasForeignKey(r => r.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
