﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Enums;

namespace ReverseAnalytics.Infrastructure.Persistence.Configurations
{
    internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder.Property(s => s.Receipt)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(s => s.Comments)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(s => s.TotalDue)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(s => s.TotalPaid)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(s => s.TotalDiscount)
                .HasColumnType("money")
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Property(s => s.SaleType)
                .HasDefaultValue(SaleType.Other);

        }
    }
}
