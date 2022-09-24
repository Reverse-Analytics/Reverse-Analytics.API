﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SupplyDetailConfiguration : IEntityTypeConfiguration<SupplyDetail>
    {
        public void Configure(EntityTypeBuilder<SupplyDetail> builder)
        {
            builder.ToTable("Supply_Detail");

            builder.HasKey(sd => sd.Id);

            builder.HasOne(sd => sd.Supply)
                .WithMany(p => p.SupplyDetails)
                .HasForeignKey(sd => sd.SupplyId);
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(sd => sd.ProductId);

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