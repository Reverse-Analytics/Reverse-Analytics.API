using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations
{
    public partial class Add_Supplier_Debt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierDebt",
                columns: table => new
                {
                    SupplierDebtId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    DebtDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDebt", x => x.SupplierDebtId);
                    table.ForeignKey(
                        name: "Supplier_FK",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierDebt_SupplierId",
                table: "SupplierDebt",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Supplier_FK",
                table: "Purchase");

            migrationBuilder.DropTable(
                name: "SupplierDebt");
        }
    }
}
