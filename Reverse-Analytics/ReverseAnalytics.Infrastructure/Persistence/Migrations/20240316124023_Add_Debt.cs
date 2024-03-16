using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations
{
    public partial class Add_Debt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Debt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SourceId = table.Column<int>(type: "INTEGER", nullable: false),
                    DebtorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDue = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Leftover = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Source = table.Column<int>(type: "INTEGER", nullable: false),
                    DebtorType = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebtPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DebtId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtPayment_Debt_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DebtPayment_DebtId",
                table: "DebtPayment",
                column: "DebtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DebtPayment");

            migrationBuilder.DropTable(
                name: "Debt");
        }
    }
}
