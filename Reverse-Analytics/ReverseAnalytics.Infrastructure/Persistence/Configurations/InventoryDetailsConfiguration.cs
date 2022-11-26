using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class InventoryDetailsConfiguration : IEntityTypeConfiguration<InventoryDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryDetail> builder)
        {
            builder.ToTable("Inventory_Detail");

            builder.HasKey(ip => ip.Id);

            builder.HasOne(ip => ip.Inventory)
                .WithMany(i => i.Products)
                .HasForeignKey(ip => ip.InventoryId);
            builder.HasOne(ip => ip.Product)
                .WithMany(p => p.InventoryProducts)
                .HasForeignKey(ip => ip.ProductId);

            builder.Property(ip => ip.ProductsRemained)
                .HasDefaultValue(0)
                .IsRequired();
            builder.Property(ip => ip.EnoughForDays)
                .HasDefaultValue(0)
                .IsRequired();
        }
    }
}
