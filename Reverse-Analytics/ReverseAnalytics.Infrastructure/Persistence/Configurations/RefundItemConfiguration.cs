using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class RefundItemConfiguration : IEntityTypeConfiguration<RefundItem>
    {
        public void Configure(EntityTypeBuilder<RefundItem> builder)
        {
            builder.ToTable("Refund_Item");

            builder.HasKey(rd => rd.Id);

            builder.HasOne(rd => rd.Refund)
                .WithMany(r => r.RefundItems)
                .HasForeignKey(rd => rd.RefundId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rd => rd.Product)
                .WithMany(r => r.RefundItems)
                .HasForeignKey(rd => rd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
