#nullable disable


using Microsoft.EntityFrameworkCore.Migrations;

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations.SQLite
{
    public partial class Update_SaleDebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Sale_Debt",
                newName: "DebtDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DebtDate",
                table: "Sale_Debt",
                newName: "DueDate");
        }
    }
}
