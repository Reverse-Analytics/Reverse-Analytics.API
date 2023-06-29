using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class RefundDetailsConfiguration : IEntityTypeConfiguration<RefundDetail>
    {
        public void Configure(EntityTypeBuilder<RefundDetail> builder)
        {
            builder.ToTable("Refund_Detail");

            builder.HasKey(x => x.Refund);

            builder.HasOne(x => x.Refund)
                .WithMany(r => r.RefundDetails)
                .HasForeignKey(x => x.RefundId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(r => r.RefundDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
