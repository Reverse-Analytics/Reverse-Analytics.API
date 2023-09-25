using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations.SQLite
{
    public partial class Update_Sale_Refund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalDiscountPercentage",
                table: "Sale",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Refund",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscountPercentage",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Refund");
        }
    }
}
