using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eden_western_food.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CatCode = table.Column<string>(type: "TEXT", nullable: false),
                    CatTitle = table.Column<string>(type: "TEXT", nullable: false),
                    CatOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArriveDt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Mobile = table.Column<string>(type: "TEXT", nullable: false),
                    CustName = table.Column<string>(type: "TEXT", nullable: false),
                    TableNo = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusInd = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuCode = table.Column<string>(type: "TEXT", nullable: false),
                    MenuTitle = table.Column<string>(type: "TEXT", nullable: false),
                    CatCode = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuCode = table.Column<string>(type: "TEXT", nullable: false),
                    Qty = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Customer_CustId",
                        column: x => x.CustId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CustId",
                table: "OrderItem",
                column: "CustId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
