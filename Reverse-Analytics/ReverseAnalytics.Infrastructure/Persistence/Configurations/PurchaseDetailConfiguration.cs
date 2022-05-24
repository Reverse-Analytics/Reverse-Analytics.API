using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.ToTable("Purchase_Detail");

            builder.HasKey(pd => pd.Id);

            builder.HasOne(pd => pd.Purchase)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.PurchaseId)
                .HasConstraintName("Purchase_FK");
            builder.HasOne(pd => pd.Product)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(pd => pd.ProductId)
                .HasConstraintName("Product_FK");

            builder.Property(p => p.Quantity)
                .IsRequired();
            builder.Property(p => p.UnitPrice)
                .HasColumnType("money")
                .IsRequired();
            builder.Property(p => p.UnitPriceDiscount)
                .IsRequired(false);
        }
    }
}
