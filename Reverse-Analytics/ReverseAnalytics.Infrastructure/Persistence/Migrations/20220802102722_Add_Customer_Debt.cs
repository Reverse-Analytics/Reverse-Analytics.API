using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations
{
    public partial class Add_Customer_Debt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerDebt",
                columns: table => new
                {
                    CustomerDebtId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    DebtDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDebt", x => x.CustomerDebtId);
                    table.ForeignKey(
                        name: "Customer_FK",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDebt_CustomerId",
                table: "CustomerDebt",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Customer_FK",
                table: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerDebt");
        }
    }
}
