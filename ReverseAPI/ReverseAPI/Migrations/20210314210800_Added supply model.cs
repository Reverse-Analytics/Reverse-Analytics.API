using Microsoft.EntityFrameworkCore.Migrations;

namespace ReverseAPI.Migrations
{
    public partial class Addedsupplymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supply_Products_ProductId",
                table: "Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Supply_Suppliers_SupplierId",
                table: "Supply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supply",
                table: "Supply");

            migrationBuilder.RenameTable(
                name: "Supply",
                newName: "Supplies");

            migrationBuilder.RenameIndex(
                name: "IX_Supply_SupplierId",
                table: "Supplies",
                newName: "IX_Supplies_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Supply_ProductId",
                table: "Supplies",
                newName: "IX_Supplies_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplies",
                table: "Supplies",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Products_ProductId",
                table: "Supplies",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Products_ProductId",
                table: "Supplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplies",
                table: "Supplies");

            migrationBuilder.RenameTable(
                name: "Supplies",
                newName: "Supply");

            migrationBuilder.RenameIndex(
                name: "IX_Supplies_SupplierId",
                table: "Supply",
                newName: "IX_Supply_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplies_ProductId",
                table: "Supply",
                newName: "IX_Supply_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supply",
                table: "Supply",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_Products_ProductId",
                table: "Supply",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_Suppliers_SupplierId",
                table: "Supply",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
