using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations.SQLite
{
    public partial class Update_Sale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Debt_Sale_SaleId",
                table: "Sale_Debt");

            migrationBuilder.DropIndex(
                name: "IX_Sale_Debt_SaleId",
                table: "Sale_Debt");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Debt_SaleId",
                table: "Sale_Debt",
                column: "SaleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Debt_Sale_SaleId",
                table: "Sale_Debt",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Debt_Sale_SaleId",
                table: "Sale_Debt");

            migrationBuilder.DropIndex(
                name: "IX_Sale_Debt_SaleId",
                table: "Sale_Debt");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Sale");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Debt_SaleId",
                table: "Sale_Debt",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Debt_Sale_SaleId",
                table: "Sale_Debt",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
