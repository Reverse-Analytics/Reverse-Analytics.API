﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReverseAnalytics.Infrastructure.Persistence;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220804145930_Update_City")]
    partial class Update_City
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressDetails")
                        .HasColumnType("TEXT");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId");

                    b.HasIndex("CityId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Tashkent");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerAddress", b =>
                {
                    b.Property<int>("CustomerAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CustomerAddressId");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerAddress");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerDebt", b =>
                {
                    b.Property<int>("CustomerDebtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DebtDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerDebtId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerDebt");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerPhone", b =>
                {
                    b.Property<int>("CustomerPhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrimary")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerPhoneId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPhone");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("DiscountTotal")
                        .HasColumnType("money");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(6);

                    b.Property<decimal>("TotalDue")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<decimal?>("UnitPriceDiscount")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order_Detail", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("money");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("money");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product_Category", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Debt")
                        .HasColumnType("money");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("money");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceivedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalDue")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Purchase", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.PurchaseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PurchaseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<decimal?>("UnitPriceDiscount")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("Purchase_Detail", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplierDebt", b =>
                {
                    b.Property<int>("SupplierDebtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime>("DebtDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SupplierDebtId");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplierDebt");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplierPhone", b =>
                {
                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPrimary")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierPhoneId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SupplierId");

                    b.ToTable("SupplierPhone");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Customer", b =>
                {
                    b.HasBaseType("ReverseAnalytics.Domain.Entities.Person");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supplier", b =>
                {
                    b.HasBaseType("ReverseAnalytics.Domain.Entities.Person");

                    b.ToTable("Supplier", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Address", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("City_FK");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerAddress", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Address", "Address")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Address_FK");

                    b.HasOne("ReverseAnalytics.Domain.Entities.Customer", "Customer")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Customer_FK");

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerDebt", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Customer", "Customer")
                        .WithMany("CustomerDebts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Customer_FK");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.CustomerPhone", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Customer", "Customer")
                        .WithMany("CustomerPhones")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Customer_FK");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Order", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Customer_FK");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Order_FK");

                    b.HasOne("ReverseAnalytics.Domain.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Product_FK");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Category_FK");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Purchase", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Purchases")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Supplier_FK");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.PurchaseDetail", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Product", "Product")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Product_FK");

                    b.HasOne("ReverseAnalytics.Domain.Entities.Purchase", "Purchase")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Purchase_FK");

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplierDebt", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SupplierDebts")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Supplier_FK");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplierPhone", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SupplierPhones")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Supplier_FK");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Customer", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("ReverseAnalytics.Domain.Entities.Customer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supplier", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("ReverseAnalytics.Domain.Entities.Supplier", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Address", b =>
                {
                    b.Navigation("CustomerAddresses");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("PurchaseDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Customer", b =>
                {
                    b.Navigation("CustomerAddresses");

                    b.Navigation("CustomerDebts");

                    b.Navigation("CustomerPhones");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Purchases");

                    b.Navigation("SupplierDebts");

                    b.Navigation("SupplierPhones");
                });
#pragma warning restore 612, 618
        }
    }
}
