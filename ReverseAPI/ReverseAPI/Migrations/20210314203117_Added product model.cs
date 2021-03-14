using Microsoft.EntityFrameworkCore.Migrations;

namespace ReverseAPI.Migrations
{
    public partial class Addedproductmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSupplier",
                table: "Suppliers",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Suppliers",
                newName: "IdSupplier");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clients",
                newName: "IdClient");
        }
    }
}
