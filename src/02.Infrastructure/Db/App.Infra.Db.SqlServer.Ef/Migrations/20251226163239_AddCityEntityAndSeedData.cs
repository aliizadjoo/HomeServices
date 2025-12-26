using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infra.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddCityEntityAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertHomeService_HomeServices_SkillsId",
                table: "ExpertHomeService");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "ExpertHomeService",
                newName: "HomeServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertHomeService_SkillsId",
                table: "ExpertHomeService",
                newName: "IX_ExpertHomeService_HomeServicesId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Experts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "تهران" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "کرج" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "شیراز" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "اصفهان" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Experts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CityId",
                table: "Orders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_CityId",
                table: "Experts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertHomeService_HomeServices_HomeServicesId",
                table: "ExpertHomeService",
                column: "HomeServicesId",
                principalTable: "HomeServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experts_Cities_CityId",
                table: "Experts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertHomeService_HomeServices_HomeServicesId",
                table: "ExpertHomeService");

            migrationBuilder.DropForeignKey(
                name: "FK_Experts_Cities_CityId",
                table: "Experts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Experts_CityId",
                table: "Experts");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Experts");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "HomeServicesId",
                table: "ExpertHomeService",
                newName: "SkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertHomeService_HomeServicesId",
                table: "ExpertHomeService",
                newName: "IX_ExpertHomeService_SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertHomeService_HomeServices_SkillsId",
                table: "ExpertHomeService",
                column: "SkillsId",
                principalTable: "HomeServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
