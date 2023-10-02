﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplyItemsConfiguration : IEntityTypeConfiguration<SupplyItem>
    {
        public void Configure(EntityTypeBuilder<SupplyItem> builder)
        {
            builder.ToTable("Supply_Item");

            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Supply)
                .WithMany(s => s.SupplyItems)
                .HasForeignKey(sd => sd.SupplyId);
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(sd => sd.ProductId);

            builder.Property(sd => sd.Quantity)
                .IsRequired();
        }
    }
}
