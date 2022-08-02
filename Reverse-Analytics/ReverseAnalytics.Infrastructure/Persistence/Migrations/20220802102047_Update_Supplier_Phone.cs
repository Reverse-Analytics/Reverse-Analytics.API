using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations
{
    public partial class Update_Supplier_Phone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierPhone",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierPhoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPhone", x => x.SupplierId);
                    table.ForeignKey(
                        name: "Supplier_FK",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Supplier_FK",
                table: "Purchase");

            migrationBuilder.DropTable(
                name: "SupplierPhone");
        }
    }
}
