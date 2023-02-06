using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");

            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.Details)
                .WithOne(ip => ip.Inventory)
                .HasForeignKey(ip => ip.InventoryId);

            builder.Property(i => i.Name)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
