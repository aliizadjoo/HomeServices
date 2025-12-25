using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infra.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderImage_And_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProposedPrice",
                table: "Proposals",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "WalletBalance",
                table: "Experts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletBalance",
                table: "Customers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRevenue",
                table: "Admins",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OrderImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderImages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalRevenue",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "WalletBalance",
                value: 100000m);

            migrationBuilder.UpdateData(
                table: "Experts",
                keyColumn: "Id",
                keyValue: 1,
                column: "WalletBalance",
                value: 200000m);

            migrationBuilder.InsertData(
                table: "OrderImages",
                columns: new[] { "Id", "CreatedAt", "ImagePath", "IsDeleted", "OrderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "apartment-cleaning-1.jpg", false, 1 },
                    { 2, new DateTime(2025, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "apartment-cleaning-2.jpg", false, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderImages_OrderId",
                table: "OrderImages",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderImages");

            migrationBuilder.DropColumn(
                name: "WalletBalance",
                table: "Experts");

            migrationBuilder.DropColumn(
                name: "WalletBalance",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TotalRevenue",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Proposals",
                newName: "ProposedPrice");
        }
    }
}
